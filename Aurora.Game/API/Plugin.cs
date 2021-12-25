using System;
using Aurora.Game.Overlays;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace Aurora.Game.API
{
    public abstract class Plugin
    {
        public abstract PluginType PluginType { get; }

        public readonly ResourceStore<byte[]> Resources;
        public readonly TextureStore Textures;

        protected Plugin()
        {
            Resources = new ResourceStore<byte[]>();
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(osu.Framework.Game).Assembly), "Resources"));
            Resources.AddStore(new DllResourceStore(GetType().Assembly));

            Textures = new TextureStore(new TextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, "Textures")));
        }

        public virtual UtilityBarButton GetButton() => throw new NotImplementedException();

        public virtual Drawable[] GetPluginDrawables() => throw new NotImplementedException();
    }
}
