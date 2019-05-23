using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace System
{
    /// <summary>
    ///     Represents a string that can contain values for multiple languages.<br/> 
    ///     You can map this class to a string property by using the Value property, which is
    ///     an XML-formatted value of the internal dictionary.<br/> 
    ///     Generated xml will look like this:<br/><br/>
    /// 
    ///     <br/>&lt;<see cref="TranslationsNode"/>&gt;
    ///     <br/>    &lt;<see cref="ValueNode"/>
    ///     <br/>           <see cref="LanguageAttribute"/>="My language here"&gt;
    ///     <br/>       &lt;![CDATA[My value here]]&gt;
    ///     <br/>    &lt;/<see cref="ValueNode"/>&gt;
    ///     <br/>&lt;/<see cref="TranslationsNode"/>&gt;
    /// 
    /// </summary>
    public class MultilingualString : IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        ///     The name of the root tag of the xml formatted value
        /// </summary>
        private const string TranslationsNode = "translations";

        /// <summary>
        ///     The name of the attribute on a <see cref="ValueNode"/> tag that indicates the language
        /// </summary>
        private const string LanguageAttribute = "language";

        /// <summary>
        ///     The name of the tag on a value that contains the value for a specific language
        /// </summary>
        private const string ValueNode = "value";

        /// <summary>
        ///     The inner dictionary where the keys are languages and the values are the value for each language.
        /// </summary>
        private IDictionary<string, string> _translations;

        #region Properties

        /// <summary>
        ///     Gets or sets the serialized XML of all <see cref="Translations" />
        /// </summary>
        public string Value { get { return ToXml(Translations); } set { Translations = FromXml(value); } }

        /// <summary>
        ///     Gets or sets the translations for this <see cref="MultilingualString"/>
        /// </summary>
        private IDictionary<string, string> Translations
        {
            get { return _translations ?? (_translations = new Dictionary<string, string>()); }
            set { _translations = value; }
        }

        /// <summary>
        ///     Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        ///     The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="key" /> is null.
        /// </exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
        ///     The property is retrieved and <paramref name="key" /> is not found.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
        /// </exception>
        public string this[string key]
        {
            get { return ContainsKey(key) ? Translations[key] : string.Empty; }
            set{Translations[key] = value;}
        }

        /// <summary>
        ///     Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the
        ///     <see
        ///         cref="T:System.Collections.Generic.IDictionary`2" />
        ///     .
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements
        ///     <see
        ///         cref="T:System.Collections.Generic.IDictionary`2" />
        ///     .
        /// </returns>
        public ICollection<string> Keys
        {
            get { return Translations.Keys; }
        }

        /// <summary>
        ///     Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the
        ///     <see
        ///         cref="T:System.Collections.Generic.IDictionary`2" />
        ///     .
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements
        ///     <see
        ///         cref="T:System.Collections.Generic.IDictionary`2" />
        ///     .
        /// </returns>
        public ICollection<string> Values
        {
            get { return Translations.Values; }
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public int Count
        {
            get { return Translations.Count; }
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return Translations.IsReadOnly; }
        }

        #endregion

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _translations.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">
        ///     The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </exception>
        public void Add(KeyValuePair<string, string> item)
        {
            Translations.Add(item);
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </exception>
        public void Clear()
        {
            Translations.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        /// <param name="item">
        ///     The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        public bool Contains(KeyValuePair<string, string> item)
        {
            return Translations.Contains(item);
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific language.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="language" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        /// <param name="language">
        ///     The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        public bool ContainsLanguage(string language)
        {
            return ContainsKey(language);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an
        ///     <see
        ///         cref="T:System.Array" />
        ///     , starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from
        ///     <see
        ///         cref="T:System.Collections.Generic.ICollection`1" />
        ///     . The <see cref="T:System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        ///     The zero-based index in <paramref name="array" /> at which copying begins.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="array" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="arrayIndex" /> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from
        ///     <paramref
        ///         name="arrayIndex" />
        ///     to the end of the destination <paramref name="array" />.
        /// </exception>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            Translations.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="item" /> was successfully removed from the
        ///     <see
        ///         cref="T:System.Collections.Generic.ICollection`1" />
        ///     ; otherwise, false. This method also returns false if
        ///     <paramref
        ///         name="item" />
        ///     is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        /// <param name="item">
        ///     The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </exception>
        public bool Remove(KeyValuePair<string, string> item)
        {
            return Translations.Remove(item);
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
        /// </returns>
        /// <param name="key">
        ///     The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="key" /> is null.
        /// </exception>
        public bool ContainsKey(string key)
        {
            return Translations.ContainsKey(key);
        }

        /// <summary>
        ///     Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="key" /> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     An element with the same key already exists in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
        /// </exception>
        public void Add(string key, string value)
        {
            Translations.Add(key, value);
        }

        /// <summary>
        ///     Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <returns>
        ///     true if the element is successfully removed; otherwise, false.  This method also returns false if
        ///     <paramref
        ///         name="key" />
        ///     was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        /// <param name="key">The key of the element to remove.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="key" /> is null.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
        /// </exception>
        public bool Remove(string key)
        {
            return Translations.Remove(key);
        }

        /// <summary>
        ///     Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        ///     true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the
        ///     <paramref
        ///         name="value" />
        ///     parameter. This parameter is passed uninitialized.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="key" /> is null.
        /// </exception>
        public bool TryGetValue(string key, out string value)
        {
            return Translations.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Returns the dictionary built from <paramref name="xmlValue"/>
        /// </summary>
        /// <param name="xmlValue">The xml formatted value</param>
        /// <returns>the dictionary built from <paramref name="xmlValue"/></returns>
        private IDictionary<string, string> FromXml(string xmlValue)
        {
            if (xmlValue == null)
                return new Dictionary<string, string>();

            var dictionary = new Dictionary<string, string>();
            foreach (XElement element in XElement.Parse(xmlValue).Elements())
            {
                string language = element.Attribute(LanguageAttribute).Value;
                string value = element.Value;
                dictionary.Add(language, value);
            }
            return dictionary;
        }

        /// <summary>
        ///     Returns the xml formatted string of the <paramref name="translations"/>.
        ///     The names of the tags are configured by <see cref="TranslationsNode"/>, <see cref="ValueNode"/> and <see cref="LanguageAttribute"/>
        /// </summary>
        /// <param name="translations">The keyvaluepairs containing the translations</param>
        /// <returns>
        ///     The xml formatted string of the <paramref name="translations"/>.
        ///     The names of the tags are configured by <see cref="TranslationsNode"/>, <see cref="ValueNode"/> and <see cref="LanguageAttribute"/>
        /// </returns>
        private string ToXml(IEnumerable<KeyValuePair<string, string>> translations)
        {
            var xmlValue = new XElement(TranslationsNode);
            foreach (var keyValuePair in translations)
            {
                var value = new XElement(ValueNode, new XCData(keyValuePair.Value));
                value.SetAttributeValue(LanguageAttribute, keyValuePair.Key);
                xmlValue.Add(value);
            }
            return xmlValue.ToString(SaveOptions.DisableFormatting);
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Translations: {0}, Value: {1}", _translations, Value);
        }
    }
}