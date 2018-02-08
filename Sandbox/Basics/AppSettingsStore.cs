using System;
using System.Configuration;

namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Represents a class that serves configuration options from an
    ///     app.config file.
    /// </summary>
    public class AppSettingsSettingStore : ISettingsStore
    {
        /// <summary>
        ///     Gets the version of changes to support observable setting
        ///     stores according to the external configuration pattern.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        ///     Updates the item stored at the specified key with the given value.
        /// </summary>
        /// <remarks>
        ///     This operation may not be supported by all stores such as AppSettings
        ///     or Cloud Configuration Manager.
        /// </remarks>
        /// <param name="key">Key of the setting to be updated or added.</param>
        /// <param name="value">Value of of the setting to be updated or added.</param>
        /// <exception cref="NotImplementedException">This method is not implemented.</exception>
        public void Update(string key, string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Key of the app setting.</param>
        /// <returns>The value of the setting.</returns>
        public string GetSetting(string key) => ConfigurationManager.AppSettings[key];
    }
}
