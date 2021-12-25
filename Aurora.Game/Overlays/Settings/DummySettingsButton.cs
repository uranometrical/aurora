using Aurora.Game.Overlays.Settings.Panels;
using Aurora.Game.Overlays.Settings.Panels.Items;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace Aurora.Game.Overlays.Settings
{
    public class DummySettingsButton : SettingsButton
    {
        public class DummySection : SettingsSection
        {
            public class DummySettingsContent : SettingsContent
            {
                [BackgroundDependencyLoader]
                private void load()
                {
                    Children = new Drawable[]
                    {
                        new SettingsCheckboxItem
                        {
                            LabelText = "THIS IS A TEST HI",
                            Current = new Bindable<bool>()
                        }
                    };
                }
            }

            public override SettingsContent GetSettings() => new DummySettingsContent();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Tooltip = "dummy";
            SetIcon("Icons/FontAwesome/gear");
        }

        public override SettingsSection GetSettings() => new DummySection();
    }
}
