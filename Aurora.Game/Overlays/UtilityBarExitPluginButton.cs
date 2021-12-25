using Aurora.Game.API;
using osu.Framework.Allocation;

namespace Aurora.Game.Overlays
{
    public class UtilityBarExitPluginButton : UtilityBarPluginButton
    {
        public UtilityBarExitPluginButton()
            : base(NullPlugin.INSTANCE)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            TooltipMain = "Exit Launcher";
            TooltipSub = "Exit out of the current launcher.";
            SetIcon("Icons/FontAwesome/x");
        }
    }
}
