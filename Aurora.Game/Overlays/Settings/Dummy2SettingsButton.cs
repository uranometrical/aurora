using Aurora.Game.Overlays.Settings.Panels;
using Aurora.Game.Overlays.Settings.Panels.Items;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;

namespace Aurora.Game.Overlays.Settings
{
    public class Dummy2SettingsButton : SettingsButton
    {
        public class Dummy2Section : SettingsSection
        {
            public class Dummy2SettingsContent : SettingsContent
            {
                [BackgroundDependencyLoader]
                private void load()
                {
                    Children = new Drawable[]
                    {
                        new SettingsCheckboxItem
                        {
                            LabelText = "THIS IS ALSO A TEST",
                            Current = new Bindable<bool>()
                        },
                        new SettingsCheckboxItem
                        {
                            LabelText = "THIS IS A TEST AS WELL",
                            Current = new Bindable<bool>()
                        }
                    };
                }
            }

            public override SettingsContent GetSettings() => new Dummy2SettingsContent();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Tooltip = "dummy";
            SetIcon("Icons/FontAwesome/gear");
        }

        public override SettingsSection GetSettings() => new Dummy2Section();
    }
}
