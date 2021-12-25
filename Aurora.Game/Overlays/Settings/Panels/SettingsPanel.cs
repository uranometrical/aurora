using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Aurora.Game.Overlays.Settings.Panels
{
    public class SettingsPanel : Container
    {
        public const float CONTENT_MARGINS = 20f;
        public const float TRANSITION_LENGTH = 600f;
        public const float PANEL_WIDTH = 400f;

        protected Container<Drawable> ContentContainer;

        protected override Container<Drawable> Content => ContentContainer;

        public SettingsPanel()
        {
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = ContentContainer = new Container<Drawable>
            {
                Width = PANEL_WIDTH,
                RelativeSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.TopRight,
                        Scale = new Vector2(2f, 1),
                        RelativeSizeAxes = Axes.Both,
                        Colour = new Colour4(0.15f, 0.15f, 0.15f, 1f),
                        Alpha = 1f
                    }
                }
            };
        }

        public override bool AcceptsFocus => true; // nah we don't need this, but idc
    }
}
