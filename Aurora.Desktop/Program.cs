using osu.Framework.Platform;
using osu.Framework;
using Aurora.Game;

namespace Aurora.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"Aurora"))
            using (osu.Framework.Game game = new AuroraGame())
                host.Run(game);
        }
    }
}
