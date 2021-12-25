using Aurora.Game.Overlays.Settings;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace Aurora.Game.Overlays
{
    public class UtilityBarSettingsButton : UtilityBarBlockingOverlayButton
    {
        protected override Anchor TooltipAnchor => Anchor.TopLeft;

        [BackgroundDependencyLoader]
        private void load(SettingsOverlay settings)
        {
            TooltipMain = "Settings";
            TooltipSub = "Open global launcher settings.";
            SetIcon("Icons/FontAwesome/gear");
            StateContainer = settings;
        }
    }
}
