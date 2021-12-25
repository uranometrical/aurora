using Aurora.Game.API;
using Aurora.Game.Graphics.Containers;
using Aurora.Game.Graphics.Utilities;
using Aurora.Game.Overlays;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Aurora.Game.Plugins.LunarClient
{
    public class LunarClientPlugin : Plugin
    {
        public static LunarClientPlugin Instance = null!;

        public override PluginType PluginType => PluginType.LauncherContent;

        public LunarClientPlugin()
        {
            Instance = this;
        }

        public override UtilityBarButton GetButton() => new LunarClientButton();

        public override Drawable[] GetPluginDrawables()
        {
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

                largeText.AddText("This is some dummy text.", creationParameters);
            }

            #endregion

            #region Small Text

            {
                void creationParameters(SpriteText x) => x.Font = AuroraFont.TorusFont;

                smallText.AddText("You currently have the Lunar Launcher open!", creationParameters);
            }

            #endregion

            return new Drawable[]
            {
                largeText,
                smallText
            };
        }
    }
}
