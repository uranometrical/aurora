using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Localisation;

namespace Aurora.Game.Graphics.Containers
{
    public class AuroraClickableContainer : ClickableContainer, IHasTooltip
    {
        private readonly Container content = new() { RelativeSizeAxes = Axes.Both };

        protected override Container<Drawable> Content => content;

        public virtual LocalisableString TooltipText { get; set; } = "";

        [BackgroundDependencyLoader]
        private void load()
        {
            if (AutoSizeAxes != Axes.None)
            {
                content.RelativeSizeAxes = Axes.Both & ~AutoSizeAxes;
                content.AutoSizeAxes = AutoSizeAxes;
            }

            InternalChildren = new Drawable[]
            {
                content
            };
        }
    }
}
