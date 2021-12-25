using Aurora.Game.API;
using Aurora.Game.Graphics.Containers;
using Aurora.Game.Graphics.Utilities;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;

namespace Aurora.Game.Screens
{
    public class LauncherScreen : Screen
    {
        [Resolved]
        private AuroraGame game { get; set; } = null!;

        public void ChangeSelectedPlugin(ValueChangedEvent<Plugin> pluginEvent)
        {
            if (pluginEvent.NewValue is NullPlugin)
            {
                ClearInternal();

                LinkTextFlowContainer largeText = new()
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.BottomCentre,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    Padding = new MarginPadding(40f),
                    Alpha = 1f,
                    Spacing = new Vector2(0f, 6f)
                };

                LinkTextFlowContainer smallText = new()
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    TextAnchor = Anchor.TopCentre,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    Padding = new MarginPadding(20f),
                    Alpha = 1f,
                    Spacing = new Vector2(0f, 2f),
                    AlwaysPresent = true
                };

                #region Large Text

                {
                    void creationParameters(SpriteText x)
                    {
                        x.Font = AuroraFont.GetFont(AuroraFont.TORUS_TYPEFACE, 46f, AuroraFont.FontWeight.Regular);
                        x.Alpha = 1f;
                    }

                    largeText.AddText("No launcher selected!", creationParameters);
                }

                #endregion

                #region Small Text

                {
                    void creationParameters(SpriteText x) => x.Font = AuroraFont.TorusFont;

                    smallText.AddText("Consider clicking on an icon in the top-right.", creationParameters);
                }

                #endregion

                AddRangeInternal(new[]
                {
                    largeText,
                    smallText
                });

                return;
            }

            ClearInternal();
            AddRangeInternal(pluginEvent.NewValue.GetPluginDrawables());
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            game.SelectedPlugin.BindValueChanged(ChangeSelectedPlugin, true);
            game.UtilityBarOverlay?.Show();
        }
    }
}
