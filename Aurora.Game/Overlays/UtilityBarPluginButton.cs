using Aurora.Game.API;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;

namespace Aurora.Game.Overlays
{
    public abstract class UtilityBarPluginButton : UtilityBarButton
    {
        protected Plugin Plugin { get; }

        [Resolved]
        private AuroraGame game { get; set; } = null!;

        protected override Anchor TooltipAnchor => Anchor.TopRight;

        protected UtilityBarPluginButton(Plugin plugin)
        {
            Plugin = plugin;
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (game.SelectedPlugin.Value == Plugin)
                return true;

            game.SelectedPlugin.Value = Plugin;
            return true;
        }
    }
}
