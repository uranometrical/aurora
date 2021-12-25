namespace Aurora.Game.API
{
    /// <summary>
    ///     A special plugin representing a new value.
    /// </summary>
    public sealed class NullPlugin : Plugin
    {
        public static readonly NullPlugin INSTANCE = new();

        public override PluginType PluginType => PluginType.LauncherContent;
    }
}
