namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Basic implementation of the IConfigurationManager.
    /// </summary>
    public class BasicConfigurationManager : IConfigurationManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BasicConfigurationManager"/> class.
        /// </summary>
        /// <param name="settingsStore">The settings store to access.</param>
        public BasicConfigurationManager(ISettingsStore settingsStore)
        {
            settingsStore.ShouldNotBeNull();
            this.SettingsStore = settingsStore;
        }

        /// <summary>
        ///     Gets the underlying setting store.
        /// </summary>
        private ISettingsStore SettingsStore { get; }

        /// <summary>
        ///     Gets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Name of the app setting.</param>
        /// <returns>The value of the setting.</returns>
        public string GetSetting(string key)
        {
            key.ShouldNotBeNullEmptyOrWhiteSpace();

            return this.SettingsStore.GetSetting(key);
        }

        /// <summary>
        ///     Sets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Key of the setting.</param>
        /// <param name="value">Value of the setting.</param>
        public void SetSetting(string key, string value)
        {
            key.ShouldNotBeNullEmptyOrWhiteSpace();
            value.ShouldNotBeNullEmptyOrWhiteSpace();

            this.SettingsStore.Update(key, value);
        }
    }
}