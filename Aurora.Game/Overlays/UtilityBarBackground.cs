using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;

namespace Aurora.Game.Overlays
{
    public class UtilityBarBackground : Container
    {
        private readonly Box gradientBackground;

        public UtilityBarBackground(Colour4 boxColour)
        {
            RelativeSizeAxes = Axes.Both;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = boxColour
                },
                gradientBackground = new Box
                {
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.BottomLeft,
                    Alpha = 0,
                    Height = 50,
                    Colour = ColourInfo.GradientVertical(new Colour4(0f, 0f, 0f, 0.9f), new Colour4(0f, 0f, 0f, 0f))
                },
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            gradientBackground.FadeIn(500D, Easing.OutQuint);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            gradientBackground.FadeOut(500D, Easing.OutQuint);
        }
    }
}
