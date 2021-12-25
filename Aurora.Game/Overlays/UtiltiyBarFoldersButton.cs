using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Platform;

namespace Aurora.Game.Overlays
{
    public class UtiltiyBarFoldersButton : UtilityBarButton
    {
        protected override Anchor TooltipAnchor => Anchor.TopLeft;

        [Resolved(canBeNull: true)]
        private Storage? storage { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            TooltipMain = "Open Folder";
            TooltipSub = "Open the root launcher folder.";
            SetIcon("Icons/FontAwesome/folder");
        }

        protected override bool OnClick(ClickEvent e)
        {
            storage?.PresentExternally();
            return true;
        }
    }
}
