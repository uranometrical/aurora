using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Aurora.Game.Graphics.Containers
{
    /// <summary>
    ///     An invisible drawable that brings multiple <see cref="Drawable"/> pieces together to form a consumable clickable link.
    /// </summary>
    public class DrawableLinkCompiler : AuroraHoverContainer
    {
        /// <summary>
        /// Each word part of a chat link (split for word-wrap support).
        /// </summary>
        public readonly List<Drawable> Parts;

        public DrawableLinkCompiler(ITextPart part)
            : this(part.Drawables.OfType<SpriteText>())
        {
        }

        public override bool ReceivePositionalInputAt(Vector2 screenSpacePos) => Parts.Any(d => d.ReceivePositionalInputAt(screenSpacePos));

        public DrawableLinkCompiler(IEnumerable<Drawable> parts)
        {
            Parts = parts.ToList();
        }

        protected override IEnumerable<Drawable> EffectTargets => Parts;
    }
}
