using Aurora.Game.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using Axes = osu.Framework.Graphics.Axes;
using Easing = osu.Framework.Graphics.Easing;

namespace Aurora.Game.Overlays.Settings.Panels.Items
{
    public class RestoreDefaultValueButton<T> : AuroraButton, IHasTooltip, IHasCurrentValue<T>
    {
        public override bool IsPresent => base.IsPresent || Scheduler.HasPendingTasks;

        // this is done to ensure a click on this button doesn't trigger focus on a parent element which contains the button.
        public override bool AcceptsFocus => true;

        // this is intentionally not using BindableWithCurrent, as it can use the wrong IsDefault implementation when passed a BindableNumber.
        // using GetBoundCopy() ensures that the received bindable is of the exact same type as the source bindable and uses the proper IsDefault implementation.
        private Bindable<T>? current;

        public Bindable<T>? Current
        {
            get => current;
            set
            {
                if (value is null)
                {
                    current?.UnbindAll();
                    current = value;
                    return;
                }

                current?.UnbindAll();
                current = value.GetBoundCopy();

                current.ValueChanged += _ => UpdateState();
                current.DefaultChanged += _ => UpdateState();
                current.DisabledChanged += _ => UpdateState();

                if (IsLoaded)
                    UpdateState();
            }
        }

        private const float size = 4;

        [BackgroundDependencyLoader]
        private void load()
        {
            BackgroundColour = Colour4.Purple;
            Size = new Vector2(3 * size);

            Content.RelativeSizeAxes = Axes.None;
            Content.Size = new Vector2(size);
            Content.CornerRadius = size / 2;

            Alpha = 0f;

            Action += () =>
            {
                if (!current.Disabled)
                    current.SetDefault();
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateState();
            FinishTransforms(true);
        }

        public LocalisableString TooltipText => "revert to default";

        protected override bool OnHover(HoverEvent e)
        {
            UpdateState();
            return false;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            UpdateState();
        }

        public void UpdateState() => Scheduler.AddOnce(updateState);

        private const double fade_duration = 200;

        private void updateState()
        {
            if (current is null)
                return;

            Enabled.Value = !Current.Disabled;

            if (!Current.Disabled)
            {
                this.FadeTo(Current.IsDefault ? 0 : 1, fade_duration, Easing.OutQuint);
                Background.FadeColour(IsHovered ? Colour4.Lime : Colour4.LimeGreen, fade_duration, Easing.OutQuint);
                Content.TweenEdgeEffectTo(new EdgeEffectParameters
                {
                    Colour = (IsHovered ? Colour4.LimeGreen : Colour4.Green).Opacity(0.4f),
                    Radius = IsHovered ? 8 : 4,
                    Type = EdgeEffectType.Glow
                }, fade_duration, Easing.OutQuint);
            }
            else
            {
                Background.FadeColour(Colour4.Green, fade_duration, Easing.OutQuint);
                Content.TweenEdgeEffectTo(new EdgeEffectParameters
                {
                    Colour = Colour4.LimeGreen.Opacity(0.1f),
                    Radius = 2,
                    Type = EdgeEffectType.Glow
                }, fade_duration, Easing.OutQuint);
            }
        }
    }
}
