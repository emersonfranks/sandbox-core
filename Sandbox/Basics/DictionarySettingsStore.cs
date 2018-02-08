using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Jupiter.Sandbox.Basics
{
    /// <summary>
    ///     Very basic settings store that uses a dicionary to manage settings.
    /// </summary>
    [Serializable]
    public class DictionarySettingsStore : Dictionary<string, string>, ISettingsStore
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DictionarySettingsStore"/> class.
        /// </summary>
        public DictionarySettingsStore()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DictionarySettingsStore"/> class.
        /// </summary>
        /// <param name="pairs">
        ///     An enumeration of items that this instance will be initialized with.
        /// </param>
        public DictionarySettingsStore(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            if (pairs == null)
            {
                return;
            }

            foreach (var pair in pairs)
            {
                this.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DictionarySettingsStore"/> class
        ///      with serialized data.
        /// </summary>
        /// <param name="info">
        ///     A <seealso cref="SerializationInfo"/> object containing the information
        ///     required to serialize the <see cref="DictionarySettingsStore"/>.
        /// </param>
        /// <param name="context">
        ///     A <see cref="StreamingContext"/> structure containing the source
        ///     and destination of the serialized stream associated with the <see cref="DictionarySettingsStore"/>.
        /// </param>
        protected DictionarySettingsStore(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///     Gets or sets the version of changes to support observable
        ///     setting stores according to the external configuration pattern.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Implements the <see cref="ISerializable"/> interface and returns
        ///     the data needed to serialize the <see cref="DictionarySettingsStore"/> instance.
        /// </summary>
        /// <param name="info">
        ///     A <see cref="SerializationInfo"/> object that contains the information
        ///     required to serialize the <see cref="DictionarySettingsStore"/> instance.
        /// </param>
        /// <param name="context">
        ///     A <see cref="StreamingContext"/> structure that contains the source
        ///     and destination of the serialized stream associated with the <see cref="DictionarySettingsStore"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">Parameter info is null.</exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Version", this.Version);
            base.GetObjectData(info, context);
        }

        /// <summary>
        ///     Sets or adds the value of a setting given its key.
        /// </summary>
        /// <param name="key">Key of the setting to be updated or added.</param>
        /// <param name="value">Value of of the setting to be updated or added.</param>
        public void Update(string key, string value)
        {
            key.ShouldNotBeNull(nameof(key));

            this[key] = value;
        }

        /// <summary>
        ///     Gets the value of a setting given its key.
        /// </summary>
        /// <param name="key">Key of the app setting.</param>
        /// <returns>The value of the setting.</returns>
        public string GetSetting(string key)
        {
            key.ShouldNotBeNull(nameof(key));

            return this.TryGetValue(key, out var value) ? value : null;
        }
    }
}
