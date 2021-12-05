using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;

namespace Aurora.Game.Graphics.Containers
{
    public class TextLink : TextPart
    {
        private ITextPart innerPart { get; }

        private LocalisableString tooltipText { get; }

        private Action action { get; }

        private ColourInfo hoverColor { get; }

        private ColourInfo idleColor { get; }

        public TextLink(ITextPart innerPart, LocalisableString tooltipText, Action action, ColourInfo? hoverColor = null, ColourInfo? idleColor = null)
        {
            this.innerPart = innerPart;
            this.tooltipText = tooltipText;
            this.action = action;
            this.hoverColor = hoverColor ?? Colour4.White;
            this.idleColor = idleColor ?? Colour4.LightBlue;
        }

        protected override IEnumerable<Drawable> CreateDrawablesFor(TextFlowContainer textFlowContainer)
        {
            if (textFlowContainer is not LinkTextFlowContainer linkTextFlowContainer)
                throw new ArgumentException("Container is not instance of LinkTextFlowContainer!");

            innerPart.RecreateDrawablesFor(linkTextFlowContainer);
            List<Drawable> drawables = innerPart.Drawables.ToList();

            drawables.Add(linkTextFlowContainer.CreateLinkCompiler(innerPart).With(x =>
            {
                x.RelativeSizeAxes = Axes.Both;
                x.TooltipText = tooltipText;
                x.Action = action;
                x.HoverColor = hoverColor;
                x.IdleColor = idleColor;
            }));

            return drawables;
        }
    }
}
