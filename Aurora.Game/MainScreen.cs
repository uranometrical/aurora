using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace Aurora.Game
{
    public class MainScreen : Screen
    {
        public class TooltipBox : Box, IHasTooltip
        {
            public LocalisableString TooltipText => "test";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new TooltipContainer()
                {
                    Colour = Color4.Violet,
                    RelativeSizeAxes = Axes.Both,
                    Child = new TooltipBox
                    {
                        Colour = Color4.Violet,
                        RelativeSizeAxes = Axes.Both,
                    }
                },
                new SpriteText
                {
                    Y = 20,
                    Text = "Main Screen",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                new SpinningBox
                {
                    Anchor = Anchor.Centre,
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            return base.OnHover(e);
        }
    }
}
