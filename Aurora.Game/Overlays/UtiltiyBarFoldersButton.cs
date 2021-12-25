using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace Aurora.Game.Overlays
{
    public class UtiltiyBarFoldersButton : UtilityBarButton
    {
        protected override Anchor TooltipAnchor => Anchor.TopLeft;

        [BackgroundDependencyLoader]
        private void load()
        {
            TooltipMain = "Folder";
            TooltipSub = "Open the root launcher folder.";
            SetIcon("Icons/FontAwesome/folder");
        }
    }
}
