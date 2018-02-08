namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Provides an interface to classes that serve as configuration stores.
    /// </summary>
    public interface ISettingsStore
    {
        /// <summary>
        ///     Gets the version of changes to support observable setting
        ///     stores according to the external configuration pattern.
        /// </summary>
        string Version { get; }

        /// <summary>
        ///     Updates the item stored at the specified key with the given value.
        /// </summary>
        /// <remarks>
        ///     This operation may not be supported by all stores such as AppSettings
        ///     or Cloud Configuration Manager.
        /// </remarks>
        /// <param name="key">Key of the setting to be updated or added.</param>
        /// <param name="value">Value of of the setting to be updated or added.</param>
        void Update(string key, string value);

        /// <summary>
        ///     Gets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Key of the app setting.</param>
        /// <returns>The value of the setting.</returns>
        string GetSetting(string key);
    }
}