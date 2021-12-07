using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using osu.Framework;
using osu.Framework.Extensions.ObjectExtensions;
using osu.Framework.Logging;
using osu.Framework.Platform;

namespace Aurora.Game.API
{
    /// <summary>
    ///     Instanced plugin loading class.
    /// </summary>
    public class PluginLoader : IDisposable
    {
        public const string PLUGIN_LIBRARY_PREFIX = "Aurora.Game.Plugins";

        public readonly Dictionary<Assembly, Type> LoadedAssemblies = new();

        public List<Plugin> LoadedPlugins = new();

        private readonly Storage? storage;

        public PluginLoader(Storage? storage)
        {
            this.storage = storage;
        }

        public void LoadPlugins()
        {
            loadFromDisk();

            AppDomain.CurrentDomain.AssemblyResolve += resolvePluginDependencyAssembly;

            Storage? pluginStorage = storage?.GetStorageForDirectory("plugins");

            if (pluginStorage != null)
                loadUserPlugins(pluginStorage);

            LoadedPlugins.Clear();
            LoadedPlugins.AddRange(LoadedAssemblies.Values
                                                   .Select(x => Activator.CreateInstance(x) as Plugin)
                                                   .Where(x => x is not null)
                                                   .Select(x => x.AsNonNull())
                                                   .ToList());
        }

        private void loadFromDisk()
        {
            try
            {
                string[] files = Directory.GetFiles(RuntimeInfo.StartupDirectory, $"{PLUGIN_LIBRARY_PREFIX}.*.dll");

                foreach (string file in files.Where(x => !Path.GetFileName(x).Contains("Tests")))
                    loadPluginFromFile(file);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Could not load plug-in from directory {RuntimeInfo.StartupDirectory}");
            }
        }

        private void loadPluginFromFile(string file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);

            if (LoadedAssemblies.Values.Any(x => Path.GetFileNameWithoutExtension(x.Assembly.Location) == fileName))
                return;

            try
            {
                addPlugin(Assembly.LoadFrom(file));
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to load plug-in {fileName}");
            }
        }

        private void addPlugin(Assembly assembly)
        {
            if (LoadedAssemblies.ContainsKey(assembly))
                return;

            if (LoadedAssemblies.Any(x => x.Key.FullName == assembly.FullName))
                return;

            try
            {
                LoadedAssemblies[assembly] = assembly.GetTypes().First(x =>
                    x.IsPublic &&
                    x.IsSubclassOf(typeof(Plugin)) &&
                    !x.IsAbstract &&
                    x.GetConstructor(Array.Empty<Type>()) != null
                );
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to add plug-in {assembly}");
            }
        }

        private Assembly? resolvePluginDependencyAssembly(object? sender, ResolveEventArgs args)
        {
            AssemblyName asm = new(args.Name);

            Assembly? domainAssembly = AppDomain.CurrentDomain.GetAssemblies()
                                                .Where(x =>
                                                {
                                                    string? name = x.GetName().Name;

                                                    if (name is null)
                                                        return false;

                                                    return args.Name.Contains(name, StringComparison.Ordinal);
                                                })
                                                .OrderByDescending(x => x.GetName().Version)
                                                .FirstOrDefault();

            return domainAssembly ?? LoadedAssemblies.Keys.FirstOrDefault(x => x.FullName == asm.FullName);
        }

        private void loadUserPlugins(Storage pluginStorage)
        {
            IEnumerable<string>? plugins = pluginStorage.GetFiles(".", $"{PLUGIN_LIBRARY_PREFIX}.*.dll");

            foreach (string? plugin in plugins.Where(x => !x.Contains("Tests")))
                loadPluginFromFile(pluginStorage.GetFullPath(plugin));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            AppDomain.CurrentDomain.AssemblyResolve -= resolvePluginDependencyAssembly;
        }
    }
}
