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
        public const float PANEL_WIDTH = 400f;

        protected Container<Drawable> ContentContainer = null!;

        protected override Container<Drawable> Content => ContentContainer;

        public Container SectionsContainer { get; private set; }

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

            Add(SectionsContainer = new Container
            {
                Masking = true,
                RelativeSizeAxes = Axes.Both,
            });
        }

        public override bool AcceptsFocus => true;

        public void AcceptNewSettings(SettingsButton button)
        {
            SectionsContainer.Clear();
            SectionsContainer.Add(button.GetSettings());
        }
    }
}
