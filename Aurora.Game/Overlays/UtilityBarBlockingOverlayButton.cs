using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace Aurora.Game.Overlays
{
    public abstract class UtilityBarBlockingOverlayButton : UtilityBarButton
    {
        private readonly Box stateBackground;
        private OverlayContainer? stateContainer;
        private readonly Bindable<Visibility> overlayState = new();

        public OverlayContainer? StateContainer
        {
            get => stateContainer;
            set
            {
                stateContainer = value;
                overlayState.UnbindBindings();

                if (stateContainer is null)
                    return;

                Action = stateContainer.ToggleVisibility;
                overlayState.BindTo(stateContainer.State);
            }
        }

        protected UtilityBarBlockingOverlayButton()
        {
            Add(stateBackground = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = new Colour4(150, 150, 150, 180),
                Blending = BlendingParameters.Additive,
                Depth = 2,
                Alpha = 0f
            });

            overlayState.ValueChanged += stateChanged;
        }

        private void stateChanged(ValueChangedEvent<Visibility> state)
        {
            switch (state.NewValue)
            {
                case Visibility.Hidden:
                    stateBackground.FadeOut(200);
                    break;

                case Visibility.Visible:
                    stateBackground.FadeIn(200);
                    break;
            }
        }
    }
}
