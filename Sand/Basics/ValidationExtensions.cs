using System.Collections.Generic;

namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Provides syntactic sugar for accessing the Validations class methods as extension methods.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        ///     Validates if a parameter is Null and throws ArgumentNullException if so.
        /// </summary>
        /// <param name="parameter">Parameter to be validated.</param>
        public static void ShouldNotBeNull(this object parameter)
        {
            Validations.ThrowIfParameterNull(parameter);
        }

        /// <summary>
        ///     Validates if a parameter is Null and throws ArgumentNullException if so.
        /// </summary>
        /// <param name="parameter">Parameter to be validated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ShouldNotBeNull(this object parameter, string parameterName)
        {
            Validations.ThrowIfParameterNull(parameter, parameterName);
        }

        /// <summary>
        ///     Validates that a parameter parameter is not null, empty parameter or just whitespaces.
        /// </summary>
        /// <param name="parameter">String parameter to be validated.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void ShouldNotBeNullEmptyOrWhiteSpace(this string parameter, string parameterName = "parameter")
        {
            Validations.ThrowIfStringIsNullEmptyOrWhiteSpace(parameter, parameterName);
        }

        /// <summary>
        ///     Helps validate that all the required settings are in place. Typically during construction.
        /// </summary>
        /// <param name="configurationManager">Configuration manager with the settungs to validate.</param>
        /// <param name="requiredSettings">Enumeration of the required settings.</param>
        /// <remarks>
        ///     It is a good practice to implement some sort of caching in the IConfigurationManager implementation, to
        ///     perform the actual retrieval just once.
        /// </remarks>
        public static void ShouldContainSettings(this IConfigurationManager configurationManager, IEnumerable<string> requiredSettings)
        {
            Validations.ThrowIfRequiredSettingsMissing(configurationManager, requiredSettings);
        }

        /// <summary>
        ///     Validates that the string argument can be parsed to a specified type.
        /// </summary>
        /// <typeparam name="T">Type it should be parsed to.</typeparam>
        /// <param name="parameter">String parameter to validate.</param>
        public static void ShouldBeParseableTo<T>(this string parameter)
        {
            Validations.ThrowIfStringCantBeParsedTo<T>(parameter);
        }
    }
}