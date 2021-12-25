using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;

namespace Aurora.Game.Overlays.Settings.Panels.Items
{
    public class SettingsCheckboxItem : SettingsItem<bool>
    {
        private LocalisableString labelText;

        protected override Drawable CreateControl() => new BasicCheckbox();

        public override LocalisableString LabelText
        {
            get => labelText;
            set => ((BasicCheckbox)Control).LabelText = labelText = value;
        }
    }
}
