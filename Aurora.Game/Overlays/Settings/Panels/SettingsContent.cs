using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Aurora.Game.Overlays.Settings.Panels
{
    public abstract class SettingsContent : FillFlowContainer
    {
        protected override Container<Drawable> Content => FlowContent;

        protected readonly FillFlowContainer FlowContent;

        protected SettingsContent()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;

            FlowContent = new FillFlowContainer
            {
                Margin = new MarginPadding { Top = SettingsSection.ITEM_SPACING },
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, SettingsSection.ITEM_SPACING),
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                FlowContent
            });
        }
    }
}
