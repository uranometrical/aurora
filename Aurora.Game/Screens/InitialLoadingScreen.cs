using Aurora.Game.API;
using Aurora.Game.Graphics.Containers;
using Aurora.Game.Graphics.Utilities;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;

namespace Aurora.Game.Screens
{
    public class InitialLoadingScreen : Screen
    {
        public Screen ScreenToExitTo { get; init; }

        protected LinkTextFlowContainer TitleText { get; private set; }

        protected LinkTextFlowContainer OurProductIsGoodISwearText { get; private set; }

        protected LinkTextFlowContainer SupporterText { get; private set; }

        protected LinkTextFlowContainer LoadingText { get; private set; }

        [Resolved]
        private Storage? storage { get; set; }

        [Resolved]
        private PluginLoader pluginLoader { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                TitleText = new LinkTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.BottomCentre,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    Padding = new MarginPadding(40f),
                    Alpha = 1f,
                    Spacing = new Vector2(0f, 6f)
                },

                OurProductIsGoodISwearText = new LinkTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.TopCentre,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    Padding = new MarginPadding(20f),
                    Alpha = 0f,
                    Spacing = new Vector2(0f, 2f),
                    Scale = Vector2.Zero,
                    AlwaysPresent = true
                },

                SupporterText = new LinkTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.BottomCentre,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Padding = new MarginPadding(20f),
                    Alpha = 0f,
                    Spacing = new Vector2(0f, 2f)
                },

                LoadingText = new LinkTextFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.CentreLeft,
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Padding = new MarginPadding(20f),
                    Alpha = 1f,
                    Spacing = new Vector2(0f, 2f)
                }
            };

            #region Title Text

            {
                void creationParameters(SpriteText x)
                {
                    x.Font = AuroraFont.GetFont(AuroraFont.TORUS_NOTCHED_TYPEFACE, 84f, AuroraFont.FontWeight.Regular);
                    x.AlwaysPresent = true;
                    x.Alpha = 0f;
                }

                TitleText.AddText("A", creationParameters);
                TitleText.AddText("u", creationParameters);
                TitleText.AddText("r", creationParameters);
                TitleText.AddText("o", creationParameters);
                TitleText.AddText("r", creationParameters);
                TitleText.AddText("a", creationParameters);

                for (int i = 0; i < TitleText.Children.Count; i++)
                {
                    int index = i;
                    Scheduler.AddDelayed(() => TitleText.Children[index].FadeIn(500D), 400D * (i + 1));
                }
            }

            #endregion

            #region Guys It's Good I Swear Text

            {
                void creationParameters(SpriteText x) => x.Font = AuroraFont.TorusFont;

                OurProductIsGoodISwearText.AddText("Redefining client launchers.", creationParameters);

                Scheduler.AddDelayed(() =>
                {
                    OurProductIsGoodISwearText.FadeInFromZero(2000D);
                    OurProductIsGoodISwearText.ScaleTo(new Vector2(1f), 2000D, Easing.OutCubic);
                }, 3000D);
            }

            #endregion

            #region Supporter Text

            {
                void creationParameters(SpriteText x) => x.Font = AuroraFont.GetFont(AuroraFont.TORUS_TYPEFACE, 16f);

                SupporterText.AddText("Crafted with ", creationParameters);
                SupporterText.AddIcon(FontAwesome.Solid.Heart, x =>
                {
                    creationParameters(x);

                    x.Colour = Colour4.PaleVioletRed;
                });
                SupporterText.AddText(" and free for everyone!", creationParameters);
                SupporterText.NewLine();

                SupporterText.AddText("Support me through ", creationParameters);
                SupporterText.AddLink(
                    "Patreon",
                    "Opens my Patreon.",
                    "https://patreon.com/tomatophile",
                    null,
                    creationParameters,
                    Color4Extensions.FromHex("#FF424D").Lighten(0.75f),
                    Color4Extensions.FromHex("#FF424D")
                );
                SupporterText.AddText("! ", creationParameters);
                SupporterText.AddIcon(FontAwesome.Brands.Patreon, x =>
                {
                    creationParameters(x);

                    x.Colour = Color4Extensions.FromHex("#FF424D");
                });

                SupporterText.NewLine();
                SupporterText.AddText("Join the ", creationParameters);
                SupporterText.AddLink("Discord",
                    "diskc,sfrodsd", "",
                    null,
                    creationParameters,
                    Color4Extensions.FromHex("#5865F2").Lighten(0.75f),
                    Color4Extensions.FromHex("#5865F2")
                );
                SupporterText.AddText(" as well! ", creationParameters);
                SupporterText.AddIcon(FontAwesome.Brands.Discord, x =>
                {
                    creationParameters(x);

                    x.Colour = Color4Extensions.FromHex("#5865F2");
                });

                Scheduler.AddDelayed(() => SupporterText.FadeInFromZero(1000D), 2500D);
            }

            #endregion
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            LoadComponentAsync(ScreenToExitTo);
            Scheduler.Add(() =>
            {
                pluginLoader.LoadPlugins();

                if (Game is AuroraGame aurora)
                {
                    foreach (Plugin plugin in pluginLoader.LoadedPlugins)
                        aurora.UtilityBarOverlay?.AddPlugin(plugin);
                }
            });

            CheckIfLoaded();
        }

        protected void CheckIfLoaded()
        {
            if (ScreenToExitTo.LoadState != LoadState.Ready || Scheduler.HasPendingTasks)
            {
                Schedule(CheckIfLoaded);
                return;
            }

            Scheduler.AddDelayed(() => this.Push(ScreenToExitTo), 3000D);
        }
    }
}
