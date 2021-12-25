using System;
using Aurora.Game.Graphics.Utilities;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;

namespace Aurora.Game.Overlays.Settings.Panels.Items
{
    public abstract class SettingsItem<T> : Container, IHasCurrentValue<T>, IHasTooltip
    {
        protected abstract Drawable CreateControl();

        protected Drawable Control { get; }

        private IHasCurrentValue<T> controlWithCurrent => (Control as IHasCurrentValue<T>)!;

        protected override Container<Drawable> Content => FlowContent;

        protected readonly FillFlowContainer FlowContent;

        private SpriteText? labelText;

        public bool ShowsDefaultIndicator = true;
        private readonly Container defaultValueIndicatorContainer;

        public LocalisableString TooltipText { get; set; }

        public virtual LocalisableString LabelText
        {
            get => labelText?.Text ?? string.Empty;
            set
            {
                if (labelText is null)
                {
                    // construct lazily for cases where the label is not needed (may be provided by the Control).
                    FlowContent.Insert(-1, labelText = new SpriteText()
                    {
                        Font = AuroraFont.GetFont()
                    });

                    updateDisabled();
                }

                labelText.Text = value;
                updateLayout();
            }
        }

        public virtual Bindable<T> Current
        {
            get => controlWithCurrent.Current;
            set => controlWithCurrent.Current = value;
        }

        public event Action? SettingChanged;

        protected SettingsItem()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Padding = new MarginPadding { Right = SettingsPanel.CONTENT_MARGINS };

            InternalChildren = new Drawable[]
            {
                defaultValueIndicatorContainer = new Container
                {
                    Width = SettingsPanel.CONTENT_MARGINS,
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding { Left = SettingsPanel.CONTENT_MARGINS },
                    Child = FlowContent = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Spacing = new Vector2(0, 10),
                        Child = Control = CreateControl(),
                    }
                }
            };

            // IMPORTANT: all bindable logic is in constructor intentionally to support "CreateSettingsControls" being used in a context it is
            // never loaded, but requires bindable storage.
            if (controlWithCurrent is null)
                throw new ArgumentException(@$"Control created via {nameof(CreateControl)} must implement {nameof(IHasCurrentValue<T>)}");

            controlWithCurrent.Current.ValueChanged += _ => SettingChanged?.Invoke();
            controlWithCurrent.Current.DisabledChanged += _ => updateDisabled();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            // intentionally done before LoadComplete to avoid overhead.
            if (ShowsDefaultIndicator)
            {
                defaultValueIndicatorContainer.Add(new RestoreDefaultValueButton<T>
                {
                    Current = controlWithCurrent.Current,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                });
                updateLayout();
            }
        }

        private void updateLayout()
        {
            bool hasLabel = labelText != null && !string.IsNullOrEmpty(labelText.Text.ToString());

            // if the settings item is providing a label, the default value indicator should be centred vertically to the left of the label.
            // otherwise, it should be centred vertically to the left of the main control of the settings item.
            defaultValueIndicatorContainer.Height = hasLabel ? labelText!.DrawHeight : Control.DrawHeight;
        }

        private void updateDisabled()
        {
            if (labelText != null)
                labelText.Alpha = controlWithCurrent.Current.Disabled ? 0.3f : 1;
        }
    }
}
