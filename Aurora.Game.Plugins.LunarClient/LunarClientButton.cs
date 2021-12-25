using Aurora.Game.Overlays;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;

//using osu.Framework.IO.Stores;

namespace Aurora.Game.Plugins.LunarClient
{
    public class LunarClientButton : UtilityBarPluginButton
    {
        public LunarClientButton()
            : base(LunarClientPlugin.Instance)
        {
        }

        [BackgroundDependencyLoader]
        private void load(AuroraGame game)
        {
            // game.Resources.AddStore(new DllResourceStore(GetType().Assembly));

            TooltipMain = "Lunar Client";
            TooltipSub = "Open the Lunar launcher.";
            SetIcon(new Sprite
            {
                Texture = LunarClientPlugin.Instance.Textures.Get("logo")
            });
        }
    }
}
