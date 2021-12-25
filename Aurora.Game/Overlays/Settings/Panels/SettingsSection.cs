using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Aurora.Game.Overlays.Settings.Panels
{
    public abstract class SettingsSection : Container
    {
        protected FillFlowContainer FlowContent;
        protected override Container<Drawable> Content => FlowContent;

        public const int ITEM_SPACING = 14;

        protected SettingsSection()
        {
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            FlowContent = new FillFlowContainer
            {
                Margin = new MarginPadding
                {
                    Top = 36
                },
                Spacing = new Vector2(0, ITEM_SPACING),
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
            };

            Child = GetSettings();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                new Container
                {
                    Padding = new MarginPadding
                    {
                        Top = 28,
                        Bottom = 40,
                    },
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        FlowContent
                    }
                },
            });
        }

        public abstract SettingsContent GetSettings();
    }
}
