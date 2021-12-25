using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aurora.Game.API;
using Aurora.Game.Overlays;
using Aurora.Game.Screens;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osu.Framework.Threading;
using osuTK.Graphics;

namespace Aurora.Game
{
    public class AuroraGame : AuroraGameBase
    {
        public const float SIDE_OVERLAY_OFFSET_RATIO = 0.05f;

        private Container overlayContent;
        private Container rightFloatingOverlayContent;
        private Container leftFloatingOverlayContent;
        private Container topMostOverlayContent;
        private Container overlayOffsetContainer;
        private Container screenContainer;
        private Container screenOffsetContainer;
        private ScreenStack screenStack;
        private Task asyncLoadStream;
        public UtilityBarOverlay? UtilityBarOverlay;
        private readonly List<FocusedOverlayContainer> focusedOverlays = new();
        private readonly List<OverlayContainer> visibleBlockingOverlays = new();

        public float UtilityBarOffset => (UtilityBarOverlay?.Position.Y ?? 0) + (UtilityBarOverlay?.DrawHeight ?? 0);

        public Bindable<Plugin> SelectedPlugin = new(NullPlugin.INSTANCE);

        /// <summary>
        ///     Whether overlays should be able to be opened launcher-wide. Value is sourced from the current active screen.
        /// </summary>
        public readonly IBindable<OverlayActivation> OverlayActivationMode = new Bindable<OverlayActivation>();

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };

            dependencies.CacheAs(this);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddRange(new Drawable[]
            {
                screenOffsetContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        screenContainer = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Children = new Drawable[]
                            {
                                screenStack = new ScreenStack
                                {
                                    RelativeSizeAxes = Axes.Both
                                }
                            }
                        }
                    }
                },
                overlayOffsetContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        overlayContent = new Container
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        rightFloatingOverlayContent = new Container
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        leftFloatingOverlayContent = new Container
                        {
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                },
                topMostOverlayContent = new Container
                {
                    RelativeSizeAxes = Axes.Both
                }
            });

            loadComponentSingleFile(UtilityBarOverlay = new UtilityBarOverlay(), topMostOverlayContent.Add);

            screenStack.Push(new InitialLoadingScreen
            {
                ScreenToExitTo = new LauncherScreen()
            });
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            screenOffsetContainer.Padding = new MarginPadding
            {
                Top = UtilityBarOffset
            };

            overlayOffsetContainer.Padding = new MarginPadding
            {
                Top = UtilityBarOffset
            };
        }

        private void updateBlockingOverlayFade() =>
            screenContainer.FadeColour(visibleBlockingOverlays.Any() ? new Color4(0.5f, 0.5f, 0.5f, 1f) : Color4.White, 500, Easing.OutQuint);

        public void AddBlockingOverlay(OverlayContainer overlay)
        {
            if (!visibleBlockingOverlays.Contains(overlay))
                visibleBlockingOverlays.Add(overlay);
            updateBlockingOverlayFade();
        }

        public void RemoveBlockingOverlay(OverlayContainer overlay) => Schedule(() =>
        {
            visibleBlockingOverlays.Remove(overlay);
            updateBlockingOverlayFade();
        });

        /// <summary>
        ///     Close all game-wide overlays.
        /// </summary>
        public void CloseAllOverlays()
        {
            foreach (var overlay in focusedOverlays)
                overlay.Hide();
        }

        /// <summary>
        ///     Queues loading the provided component in sequential fashion. <br />
        ///     This operation is limited to a single thread to avoid saturating all cores.
        /// </summary>
        /// <param name="component">The component to load.</param>
        /// <param name="loadCompleteAction">An action to invoke on load completion (generally to add the component to the hierarchy).</param>
        /// <param name="cache">Whether to cache the component as type <typeparamref name="T"/> into the game dependencies before any scheduling.</param>
        private T loadComponentSingleFile<T>(T component, Action<T> loadCompleteAction, bool cache = false) where T : Drawable
        {
            if (cache)
                dependencies.CacheAs(component);

            if (component is FocusedOverlayContainer overlay)
                focusedOverlays.Add(overlay);

                        // schedule is here to ensure that all component loads are done after LoadComplete is run (and thus all dependencies are cached).
            // with some better organisation of LoadComplete to do construction and dependency caching in one step, followed by calls to loadComponentSingleFile,
            // we could avoid the need for scheduling altogether.
            Schedule(() =>
            {
                var previousLoadStream = asyncLoadStream;

                // chain with existing load stream
                asyncLoadStream = Task.Run(async () =>
                {
                    if (previousLoadStream != null)
                        await previousLoadStream.ConfigureAwait(false);

                    try
                    {
                        Logger.Log($"Loading {component}...");

                        // Since this is running in a separate thread, it is possible for OsuGame to be disposed after LoadComponentAsync has been called
                        // throwing an exception. To avoid this, the call is scheduled on the update thread, which does not run if IsDisposed = true
                        Task task = null;
                        var del = new ScheduledDelegate(() => task = LoadComponentAsync(component, loadCompleteAction));
                        Scheduler.Add(del);

                        // The delegate won't complete if OsuGame has been disposed in the meantime
                        while (!IsDisposed && !del.Completed)
                            await Task.Delay(10).ConfigureAwait(false);

                        // Either we're disposed or the load process has started successfully
                        if (IsDisposed)
                            return;

                        Debug.Assert(task != null);

                        await task.ConfigureAwait(false);

                        Logger.Log($"Loaded {component}!");
                    }
                    catch (OperationCanceledException)
                    {
                    }
                });
            });

            return component;
        }
    }
}
