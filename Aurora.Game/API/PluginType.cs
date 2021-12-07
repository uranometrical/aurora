namespace Aurora.Game.API
{
    /// <summary>
    ///     Indicates the type of plug-in functionality your plug-in holds.
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        ///     Means that your plug-in introduces a new launcher section (i.e. makes it possible to launch a PvP client).
        /// </summary>
        LauncherContent,

        /// <summary>
        ///     Means that your plug-in only gets loaded with the purpose of making tweaks to code or events.
        /// </summary>
        LauncherModification
    }
}
