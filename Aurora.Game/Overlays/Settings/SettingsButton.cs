using Aurora.Game.Graphics.Containers;
using Aurora.Game.Graphics.Utilities;
using Aurora.Game.Overlays.Settings.Panels;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.EnumExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osuTK;
using osuTK.Graphics;

namespace Aurora.Game.Overlays.Settings
{
    public abstract class SettingsButton : AuroraClickableContainer
    {
        [Resolved]
        private TextureStore textures { get; set; } = null!;

        public virtual void SetIcon(Drawable icon)
        {
            IconContainer.Icon = icon;
            IconContainer.Show();
        }

        public virtual void SetIcon(string texture) => SetIcon(new Sprite
        {
            Texture = textures.Get(texture)
        });

        public Drawable Icon
        {
            get => IconContainer.Icon;
            set => SetIcon(value);
        }

        public LocalisableString Text
        {
            get => DrawableText.Text;
            set => DrawableText.Text = value;
        }

        public LocalisableString Tooltip
        {
            get => tooltip2.Text;
            set => tooltip2.Text = value;
        }

        protected Anchor TooltipAnchor = Anchor.TopLeft;

        protected ConstrainedIconContainer IconContainer;
        protected SpriteText DrawableText;
        protected Box HoverBackground;
        private readonly Box flashBackground;
        private readonly FillFlowContainer tooltipContainer;
        private readonly SpriteText tooltip2;

        [Resolved]
        private SettingsOverlay settings { get; set; }

        protected SettingsButton()
        {
            Width = UtilityBarOverlay.SIZE;
            RelativeSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                HoverBackground = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(80, 80, 80, 255).Opacity(180),
                    Blending = BlendingParameters.Additive,
                    Alpha = 0,
                },
                flashBackground = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    Colour = Color4.White.Opacity(100),
                    Blending = BlendingParameters.Additive,
                },
                new FillFlowContainer
                {
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(5),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Padding = new MarginPadding { Left = SettingsOverlay.SIZE / 2f, Right = SettingsOverlay.SIZE / 2f },
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        IconContainer = new ConstrainedIconContainer
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(26f),
                            Alpha = 0,
                            Colour = Color4.White
                        },
                        DrawableText = new SpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Font = AuroraFont.GetFont()
                        }
                    },
                },
                tooltipContainer = new FillFlowContainer
                {
                    Direction = FillDirection.Vertical,
                    RelativeSizeAxes = Axes.Both, // stops us being considered in parent's autosize
                    Anchor = TooltipAnchor.HasFlagFast(Anchor.x0) ? Anchor.BottomLeft : Anchor.BottomRight,
                    Origin = TooltipAnchor,
                    Position = new Vector2(TooltipAnchor.HasFlagFast(Anchor.x0) ? 5 : -5, 5),
                    Alpha = 0,
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            AutoSizeAxes = Axes.Both,
                            Anchor = TooltipAnchor,
                            Origin = TooltipAnchor,
                            Direction = FillDirection.Horizontal,
                            Children = new[]
                            {
                                tooltip2 = new SpriteText { Shadow = true, Font = AuroraFont.GetFont() },
                                new SpriteText { Shadow = true, Font = AuroraFont.GetFont() }
                            }
                        }
                    }
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;

        protected override bool OnClick(ClickEvent e)
        {
            flashBackground.FadeOutFromOne(800, Easing.OutQuint);
            tooltipContainer.FadeOut(100);

            settings.AcceptNewSettings(this);

            return base.OnClick(e);
        }

        protected override bool OnHover(HoverEvent e)
        {
            HoverBackground.FadeIn(200);
            tooltipContainer.FadeIn(100);

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            HoverBackground.FadeOut(200);
            tooltipContainer.FadeOut(100);
        }

        public abstract SettingsSection GetSettings();
    }
}
