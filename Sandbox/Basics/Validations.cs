using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Provides common validations.
    /// </summary>
    public static class Validations
    {
        /// <summary>
        ///     Validates if a parameter is Null and throws ArgumentNullException if so.
        /// </summary>
        /// <param name="parameter">Parameter to be validated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ThrowIfParameterNull(object parameter, string parameterName = "parameter")
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        ///     Validates that a parameter parameter is not null, empty parameter or just whitespaces.
        /// </summary>
        /// <param name="parameter">String parameter to be validated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ThrowIfStringIsNullEmptyOrWhiteSpace(string parameter, string parameterName = null)
        {
            if (string.IsNullOrEmpty(parameter) || string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        ///     Helps validate that all the required configurationManager are in place. Typically during construction.
        /// </summary>
        /// <param name="configurationManager">Configuration manager with the settings to validate.</param>
        /// <param name="requiredSettings">Enumeration of the required configurationManager.</param>
        /// <remarks>
        ///     It is a good practice to implement some sort of caching in the IConfigurationManager implementation, to
        ///     perform the actual retrieval just once.
        /// </remarks>
        public static void ThrowIfRequiredSettingsMissing(IConfigurationManager configurationManager, IEnumerable<string> requiredSettings)
        {
            ThrowIfParameterNull(configurationManager, nameof(configurationManager));
            ThrowIfParameterNull(requiredSettings, nameof(requiredSettings));

            var listOfMissingSettings = new List<string>();

            foreach (var requiredSetting in requiredSettings)
            {
                if (string.IsNullOrEmpty(configurationManager.GetSetting(requiredSetting)))
                {
                    listOfMissingSettings.Add(requiredSetting);
                }
            }

            if (listOfMissingSettings.Any())
            {
                throw new ArgumentException(ConstructMissingSettingsMessage(listOfMissingSettings));
            }
        }

        /// <summary>
        ///     Validates that all the requiredSettings are present in the provided <see cref="ISettingsStore"/> instance.
        /// </summary>
        /// <param name="settingsStore">The <see cref="ISettingsStore"/> that will be validated.</param>
        /// <param name="requiredSettings">An Enumeration that contains the settings to look for in the provided settingsStore.</param>
        public static void ThrowIfRequiredSettingsMissing(ISettingsStore settingsStore, IEnumerable<string> requiredSettings)
        {
            ThrowIfParameterNull(settingsStore, nameof(settingsStore));
            ThrowIfParameterNull(requiredSettings, nameof(requiredSettings));

            var listOfMissingSettings = new List<string>();

            foreach (var requiredSetting in requiredSettings)
            {
                if (string.IsNullOrEmpty(settingsStore.GetSetting(requiredSetting)))
                {
                    listOfMissingSettings.Add(requiredSetting);
                }
            }

            if (listOfMissingSettings.Any())
            {
                throw new ArgumentException(ConstructMissingSettingsMessage(listOfMissingSettings));
            }
        }

        /// <summary>
        ///     Validates that the parameter argument can be parsed to a specified type.
        /// </summary>
        /// <typeparam name="T">Type it should be parsed to.</typeparam>
        /// <param name="parameter">String parameter to validate.</param>
        public static void ThrowIfStringCantBeParsedTo<T>(string parameter)
        {
            ThrowIfParameterNull(parameter);

            try
            {
                TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(parameter);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"The string value '{parameter}' cannot be parsed to {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        ///     Helps construct the message to be displayed by ThrowIfRequiredSettingsMissing() in case there are missing settings.
        /// </summary>
        /// <param name="missingRequiredSettings">Enumeration of missing settings.</param>
        /// <returns>A user-friendly message with the missing settings.</returns>
        private static string ConstructMissingSettingsMessage(IEnumerable<string> missingRequiredSettings)
        {
            var requiredSettings = missingRequiredSettings as string[] ?? missingRequiredSettings.ToArray();

            return requiredSettings.Length == 1
                ? $"The following setting is required but is missing from the settings source: {requiredSettings.First()}"
                : $"The following settings are required and not found in the settings source: {string.Join(",", requiredSettings)}";
        }
    }
}