namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Represents and object that manages the configuration exposed by an ISettingsStore.
    /// </summary>
    /// <remarks>
    ///     Implement in a class that can manage observable changes and the external configuration pattern.
    /// </remarks>
    public interface IConfigurationManager
    {
        /// <summary>
        ///     Gets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Name of the app setting.</param>
        /// <returns>The value of the setting.</returns>
        string GetSetting(string key);

        /// <summary>
        ///     Sets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Key of the setting.</param>
        /// <param name="value">Value of the setting.</param>
        void SetSetting(string key, string value);
    }
}