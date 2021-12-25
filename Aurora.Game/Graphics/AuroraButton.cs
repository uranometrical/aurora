using System.Diagnostics;
using Aurora.Game.Graphics.Utilities;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK.Graphics;

namespace Aurora.Game.Graphics
{
    public class AuroraButton : Button
    {
        public LocalisableString Text
        {
            get => SpriteText?.Text ?? default;
            set
            {
                if (SpriteText is not null)
                    SpriteText.Text = value;
            }
        }

        private Color4? backgroundColour;

        public Color4 BackgroundColour
        {
            get => backgroundColour ?? Color4.White;
            set
            {
                backgroundColour = value;
                Background.FadeColour(value);
            }
        }

        protected override Container<Drawable> Content { get; }

        protected Box Hover;
        protected Box Background;
        protected SpriteText? SpriteText;

        public AuroraButton()
        {
            Height = 40;

            AddInternal(Content = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Masking = true,
                CornerRadius = 5,
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    Background = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                    },
                    Hover = new Box
                    {
                        Alpha = 0,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.White.Opacity(.1f),
                        Blending = BlendingParameters.Additive,
                        Depth = float.MinValue
                    },
                    SpriteText = CreateText(),
                }
            });

            Enabled.BindValueChanged(enabledChanged, true);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (backgroundColour is null)
                BackgroundColour = Colour4.Green;

            Enabled.ValueChanged += enabledChanged;
            Enabled.TriggerChange();
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (Enabled.Value)
            {
                Debug.Assert(backgroundColour != null);
                Background.FlashColour(backgroundColour.Value, 200);
            }

            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (Enabled.Value)
                Hover.FadeIn(200, Easing.OutQuint);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            Hover.FadeOut(300);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Content.ScaleTo(0.9f, 4000, Easing.OutQuint);
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            Content.ScaleTo(1, 1000, Easing.OutElastic);
            base.OnMouseUp(e);
        }

        protected virtual SpriteText CreateText() => new()
        {
            Depth = -1f,
            Origin = Anchor.Centre,
            Anchor = Anchor.Centre,
            Font = AuroraFont.GetFont(weight: AuroraFont.FontWeight.Bold)
        };

        private void enabledChanged(ValueChangedEvent<bool> e)
        {
            this.FadeColour(e.NewValue ? Color4.White : Color4.Gray, 200, Easing.OutQuint);
        }
    }
}
