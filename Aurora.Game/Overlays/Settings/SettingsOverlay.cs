using Aurora.Game.Overlays.Settings.Panels;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Aurora.Game.Overlays.Settings
{
    public class SettingsOverlay : FocusedOverlayContainer
    {
        public const float SIZE = 60f;

        private SettingsPanel panel = null!;
        private SettingsButtonContainer buttonContainer = null!;

        public SettingsOverlay()
        {
            RelativeSizeAxes = Axes.Both;
            AlwaysPresent = true;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                panel = new SettingsPanel(),
                buttonContainer = new SettingsButtonContainer()
            };
        }

        protected override void PopIn()
        {
            buttonContainer.MoveToY(0f, 500D, Easing.OutQuint);
            panel.MoveToX(0f, 500D, Easing.OutQuint);
            this.FadeIn(500D, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            buttonContainer.MoveToY(-buttonContainer.DrawSize.Y, 500D, Easing.OutQuint);
            panel.MoveToX(-panel.DrawSize.X, 500D, Easing.OutQuint);
            this.FadeOut(500D, Easing.InQuint);
        }

        public void AcceptNewSettings(SettingsButton button) => panel.AcceptNewSettings(button);
    }
}
