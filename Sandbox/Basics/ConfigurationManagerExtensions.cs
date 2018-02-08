using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Contains enhancements for the configuration manager.
    /// </summary>
    public static class ConfigurationManagerExtensions
    {
        /// <summary>
        ///     Helps validate that all the required settings are in place. Typically during construction.
        /// </summary>
        /// <param name="configurationManager">Configuration manager with the settungs to validate.</param>
        /// <param name="requiredSettings">Enumeration of the required settings.</param>
        public static void ShouldContainSettings(
            this IConfigurationManager configurationManager,
            params object[] requiredSettings)
            => ShouldContainSettings(configurationManager, requiredSettings as IEnumerable<object>);

        /// <summary>
        ///     Helps validate that all the required settings are in place. Typically during construction.
        /// </summary>
        /// <param name="configurationManager">Configuration manager with the settungs to validate.</param>
        /// <param name="requiredSettings">Enumeration of the required settings.</param>
        public static void ShouldContainSettings(
            this IConfigurationManager configurationManager,
            IEnumerable<object> requiredSettings)
        {
            var requiredSettingsAsString =
                requiredSettings as IEnumerable<string> ?? requiredSettings.Select(s => s.ToString());

            configurationManager.ShouldContainSettings(requiredSettingsAsString);
        }

        /// <summary>
        ///     Gets the setting cast to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert the setting to.</typeparam>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="key">The key.</param>
        /// <returns>The setting converted to type T.</returns>
        public static T GetSetting<T>(this IConfigurationManager configurationManager, object key)
        {
            var keyString = key?.ToString();
            var setting = configurationManager.GetSetting(keyString);

            // Convert.ChangeType cannot inherently deal with nullables
            if (string.IsNullOrWhiteSpace(setting) && default(T) == null)
            {
                return default(T);
            }

            var targetConversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            try
            {
                return (T) Convert.ChangeType(setting, targetConversionType, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new SettingConversionException($"Unable to convert setting '{key}' to '{typeof(T)}'", ex);
            }
        }

        /// <summary>
        ///     Gets the setting at the specified key; if no setting is found
        ///     or the value is not parseable the default value is returned instead.
        /// </summary>
        /// <typeparam name="T">The type of value expected at the given key.</typeparam>
        /// <param name="configurationManager">The configuration manager.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The configuration value; default value if not found or not parseable.
        /// </returns>
        public static T GetSettingOrDefault<T>(
            this IConfigurationManager configurationManager,
            object key,
            T defaultValue)
        {
            try
            {
                var setting = configurationManager.GetSetting<T>(key);

                return default(T) == null && setting == null ? defaultValue : setting;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}