using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace Aurora.Game.Overlays
{
    public class UtilityBarSettingsButton : UtilityBarButton
    {
        protected override Anchor TooltipAnchor => Anchor.TopLeft;

        [BackgroundDependencyLoader]
        private void load()
        {
            TooltipMain = "Settings";
            TooltipSub = "Open global launcher settings.";
            SetIcon("Icons/FontAwesome/gear");
        }
    }
}
