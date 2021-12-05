using osu.Framework.Platform;
using osu.Framework;
using Aurora.Game;

namespace Aurora.Desktop
{
    public static class Program
    {
        public const string HOST = "Aurora";

        public class AuroraDesktop : AuroraGame
        {
            public override void SetHost(GameHost host)
            {
                base.SetHost(host);

                Window.Title = HOST;
            }
        }

        public static void Main()
        {
            using GameHost host = Host.GetSuitableHost(HOST);
            using osu.Framework.Game game = new AuroraDesktop();
            host.Run(game);
        }
    }
}
