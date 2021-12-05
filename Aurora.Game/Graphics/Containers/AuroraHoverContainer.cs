using System.Collections.Generic;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Input.Events;

namespace Aurora.Game.Graphics.Containers
{
    public class AuroraHoverContainer : AuroraClickableContainer
    {
        public const float FADE_DURATION = 500f;

        public ColourInfo HoverColor;
        public ColourInfo IdleColor;
        protected bool InternalIsHovered;

        protected virtual IEnumerable<Drawable> EffectTargets => new[] { Content };

        public AuroraHoverContainer()
        {
            Enabled.ValueChanged += e =>
            {
                if (!InternalIsHovered)
                    return;

                if (e.NewValue)
                    fadeIn();
                else
                    fadeOut();
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            if (InternalIsHovered)
                return false;

            InternalIsHovered = true;

            if (!Enabled.Value)
                return false;

            fadeIn();

            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (!InternalIsHovered)
                return;

            InternalIsHovered = false;
            fadeOut();

            base.OnHoverLost(e);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            EffectTargets.ForEach(x => x.FadeColour(IdleColor));
        }

        private void fadeIn() => EffectTargets.ForEach(d => d.FadeColour(HoverColor, FADE_DURATION, Easing.OutQuint));

        private void fadeOut() => EffectTargets.ForEach(d => d.FadeColour(IdleColor, FADE_DURATION, Easing.OutQuint));
    }
}
