using Aurora.Game.API;
using Aurora.Game.Overlays;

namespace Aurora.Game.Plugins.LunarClient
{
    public class LunarClientPlugin : Plugin
    {
        public static LunarClientPlugin Instance = null!;

        public override PluginType PluginType => PluginType.LauncherContent;

        public LunarClientPlugin()
        {
            Instance = this;
        }

        public override UtilityBarButton GetButton() => new LunarClientButton();
    }
}
