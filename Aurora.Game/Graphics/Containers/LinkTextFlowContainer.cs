using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Framework.Platform;

namespace Aurora.Game.Graphics.Containers
{
    public class LinkTextFlowContainer : IconTextFlowContainer
    {
        [Resolved]
        private GameHost host { get; set; }

        public virtual DrawableLinkCompiler CreateLinkCompiler(ITextPart textPart) => new(textPart);

        // We want the compilers to always be visible no matter where they are, so RelativeSizeAxes is used.
        // However due to https://github.com/ppy/osu-framework/issues/2073, it's possible for the compilers to be relative size in the flow's auto-size axes - an unsupported operation.
        // Since the compilers don't display any content and don't affect the layout, it's simplest to exclude them from the flow.
        public override IEnumerable<Drawable> FlowingChildren => base.FlowingChildren.Where(c => c is not DrawableLinkCompiler);

        public void AddLink(
            LocalisableString text,
            LocalisableString tooltip,
            string url,
            Action action = null,
            Action<SpriteText> creationParameters = null,
            ColourInfo? hoverColor = null,
            ColourInfo? idleColor = null
        ) => createLink(CreateChunkFor(text, true, CreateSpriteText, creationParameters), url, tooltip, action, hoverColor, idleColor);

        private void createLink(
            ITextPart textPart,
            string link,
            LocalisableString tooltipText,
            Action action = null,
            ColourInfo? hoverColor = null,
            ColourInfo? idleColor = null)
        {
            Action onClickAction = () =>
            {
                if (action != null)
                    action();
                else
                    host.OpenUrlExternally(link);
            };

            AddPart(new TextLink(textPart, tooltipText, onClickAction, hoverColor, idleColor));
        }
    }
}
