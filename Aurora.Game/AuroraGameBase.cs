using Aurora.Game.API;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using Aurora.Resources;
using osu.Framework.Platform;

namespace Aurora.Game
{
    public class AuroraGameBase : osu.Framework.Game
    {
        // Anything in this class is shared between the test browser and the game implementation.
        // It allows for caching global dependencies that should be accessible to tests, or changing
        // the screen scaling for all components including the test browser and framework overlays.

        protected override Container<Drawable> Content { get; }

        protected Storage? Storage { get; set; }

        private DependencyContainer dependencies;
        private PluginLoader pluginLoader;

        protected AuroraGameBase()
        {
            // Ensure game and tests scale with window size and screen DPI.
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                // You may want to change TargetDrawSize to your "default" resolution, which will decide how things scale and position when using absolute coordinates.
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(AuroraResources).Assembly));

            dependencies.CacheAs(Storage);

            pluginLoader = new PluginLoader(Storage);
            dependencies.CacheAs(pluginLoader);

            addFonts();
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            Storage ??= host.Storage;
        }

        private void addFonts()
        {
            AddFont(Resources, "Fonts/Torus-Bold");
            AddFont(Resources, "Fonts/Torus-Light");
            AddFont(Resources, "Fonts/Torus-Regular");
            AddFont(Resources, "Fonts/Torus-SemiBold");
            AddFont(Resources, "Fonts/TorusNotched-Regular");
        }
    }
}
