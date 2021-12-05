using System;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace Aurora.Game.Graphics.Containers
{
    public class IconTextFlowContainer : TextFlowContainer
    {
        public ITextPart AddIcon(IconUsage icon, Action<SpriteText> creationParameters = null) => AddText(icon.Icon.ToString(), creationParameters);
    }
}
