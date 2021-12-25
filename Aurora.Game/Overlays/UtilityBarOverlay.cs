using System.Collections.Generic;
using System.Linq;
using Aurora.Game.API;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Aurora.Game.Overlays
{
    public class UtilityBarOverlay : OverlayContainer
    {
        public const float SIZE = 60f;

        public UtilityBarOverlay()
        {
            RelativeSizeAxes = Axes.X;
            Size = new Vector2(1f, SIZE);
            AlwaysPresent = true;
        }

        public FillFlowContainer LauncherFlowContainer = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new UtilityBarBackground(),
                LauncherFlowContainer = new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new UtilityBarExitPluginButton()
                    }
                },
                new FillFlowContainer
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new UtilityBarSettingsButton(),
                        new UtiltiyBarFoldersButton()
                    }
                }
            };
        }

        protected override void PopIn()
        {
            this.MoveToY(0, 500, Easing.OutQuint);
            this.FadeIn(500D / 4D, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            // userButton.StateContainer?.Hide();

            this.MoveToY(-DrawSize.Y, 500, Easing.OutQuint);
            this.FadeOut(500, Easing.InQuint);
        }

        public void AddPlugin(Plugin plugin)
        {
            if (plugin.PluginType != PluginType.LauncherContent)
                return;

            Scheduler.Add(() =>
            {
                // we prepend instead so our initial buttons (x, etc.) are to the right.
                IEnumerable<Drawable> children = LauncherFlowContainer.Children.ToList(); // ToList() to get a new enumerable
                children = children.Prepend(plugin.GetButton());
                LauncherFlowContainer.Clear(false); // don't dispose children
                LauncherFlowContainer.AddRange(children);
            });
        }
    }
}
