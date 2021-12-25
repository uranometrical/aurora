using osu.Framework.Allocation;

namespace Aurora.Game.Overlays.Settings
{
    public class DummySettingsButton : SettingsButton
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Tooltip = "dummy";
            SetIcon("Icons/FontAwesome/gear");
        }
    }
}
