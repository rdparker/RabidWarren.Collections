// -----------------------------------------------------------------------
//  <copyright file="Multimap.cs" company="Ron Parker">
//   Copyright 2014 Ron Parker
//  </copyright>
//  <summary>
//   Implements a map of keys to one or more values.
//  </summary>
// -----------------------------------------------------------------------

namespace RabidWarren.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a map of keys to one or more values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values</typeparam>
    public class Multimap<TKey, TValue> : IMultimap<TKey, TValue>
    {
        /// <summary>
        /// The the backing dictionary for the map.
        /// <para>The <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> is to provide efficient
        /// lookup of a relatively small number of items per key.  The original use case had class types as the key and
        /// property metadata as the values.  This did not contain the value data for the properties within instances
        /// of the classes, so the number of items per key was of small magnitude.</para>
        /// <para>So, the the keys are maintained in a dictionary and there values are stored in a simple
        /// <see cref="System.Collections.Generic.List{TValue}"/>.</para>
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        private Dictionary<TKey, List<TValue>> _dictionary = new Dictionary<TKey, List<TValue>>();

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        /// <value>The number of elements contained in the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.</value>
        public int Count
        {
            get { return _dictionary.Sum(x => x.Value.Count); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Adds an element with the provided key and value to the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public void Add(TKey key, TValue value)
        {
            VerifyKey(key);
            List<TValue> entries;

            if (!_dictionary.TryGetValue(key, out entries))
            {
                entries = new List<TValue>();
                _dictionary.Add(key, entries);
            }

            entries.Add(value);
        }

        /// <summary>
        /// Determines whether the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> contains an
        /// element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> contains an element
        /// with the key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public bool ContainsKey(TKey key)
        {
            VerifyKey(key);

            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Removes the element with the specified key and value from the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The value of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if the <paramref name="key"/> and matching <paramref name="value"/> were not found
        /// in the original <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public bool Remove(TKey key, TValue value)
        {
            VerifyKey(key);
            List<TValue> entries;

            return _dictionary.TryGetValue(key, out entries) && entries.Remove(value);
        }

        /// <summary>
        /// Removes all elements with the specified key from the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the elements to remove.</param>
        /// <returns><c>true</c> if the elements are successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if the <paramref name="key"/> was not found in the original
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public bool RemoveAll(TKey key)
        {
            VerifyKey(key);
            return _dictionary.Remove(key);
        }

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose values should be gotten.</param>
        /// <param name="values">When this method returns, the values associated with the specified key, if they key
        /// is found; otherwise, at empty collection is returned. The parameter should be passed uninitialized. It
        /// will not be updated, it will be replaced.</param>
        /// <returns><c>true</c> if the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> object
        /// contains any elements with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        public bool TryGetValues(TKey key, out ICollection<TValue> values)
        {
            VerifyKey(key);
            List<TValue> output;

            var result = _dictionary.TryGetValue(key, out output);
            
            values = output;

            return result;
        }

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <param name="item">
        /// The key-value pair to add to the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all items from the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> contains
        /// a specific key-value pair.
        /// </summary>
        /// <param name="pair">The key-value pair to look for.</param>
        /// <returns><c>true</c> if this instance contains <paramref name="pair"/>; otherwise, <c>false</c>.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            List<TValue> entries;

            return _dictionary.TryGetValue(pair.Key, out entries) && entries.Contains(pair.Value);
        }

        /// <summary>
        /// Copies the elements of the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> to a
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements
        /// copied from <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>. The
        /// <see cref="System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/>at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="System.ArgumentException">The number of elements in the source
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/> is greater than the available space
        /// from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            
            var count = Count;
            if (count > 0 && arrayIndex + count >= array.Length)
            {
                var message =
                    string.Format(
                        "Copying {0} elements into the {1} element array beginning at {2} would exceed its length.",
                        count, 
                        array.Length,
                        arrayIndex);

                throw new ArgumentException(message);
            }

            foreach (var keyValuePair in this)
            {
                array[arrayIndex] = keyValuePair;
                arrayIndex++;
            }
        }

        /// <summary>
        /// Removes the first occurrence of the specific object from the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if the <paramref name="item"/> was not found in the original
        /// <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key, item.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(_dictionary);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Verifies the key is not null.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        private static void VerifyKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
        }

        /// <summary>
        /// Represents an enumerator for the <see cref="RabidWarren.Collections.Generic.Multimap{TKey, TValue}"/>
        /// class.
        /// </summary>
        private class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator, IEnumerator
        {
            /// <summary>
            /// The backing dictionary for the collection being enumerated.
            /// </summary>
            private Dictionary<TKey, List<TValue>> _dictionary;

            /// <summary>
            /// The enumerator for the backing dictionary keys.
            /// </summary>
            private Dictionary<TKey, List<TValue>>.Enumerator _keys;

            /// <summary>
            /// The enumerator for the values of the currently enumerated key.
            /// </summary>
            private List<TValue>.Enumerator _values;

            /// <summary>
            /// Was the enumerator just reset?
            /// </summary>
            private bool _justReset;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> class.
            /// </summary>
            /// <param name="dictionary">The collections backing dictionary.</param>
            public Enumerator(Dictionary<TKey, List<TValue>> dictionary)
            {
                _dictionary = dictionary;
                Reset();
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(_keys.Current.Key, _values.Current);
                }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(_keys.Current.Key, _values.Current);
                }
            }

            /// <summary>
            /// Gets the key of the current dictionary entry.
            /// </summary>
            public object Key
            {
                get { return _keys.Current.Key; }
            }

            /// <summary>
            /// Gets the value of the current dictionary entry.
            /// </summary>
            public object Value
            {
                get { return _values.Current; }
            }

            /// <summary>
            /// Gets both the key and the value of the current dictionary entry.
            /// </summary>
            public DictionaryEntry Entry
            {
                get { return new DictionaryEntry(_keys.Current.Key, _values.Current); }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// <c>true</c> if the enumerator was successfully advanced to the next element; otherwise, <c>false</c>
            /// if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (!_justReset && _values.MoveNext())
                {
                    _justReset = false;
                    return true;
                }

                _justReset = false;
                if (!_keys.MoveNext())
                    return false;
               
                _values = _keys.Current.Value.GetEnumerator();

                // Prime the new _values enumerator so that Entry, will return a valid key-value pair.
                return _values.MoveNext();
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _keys = _dictionary.GetEnumerator();
                _justReset = true;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                _keys.Dispose();
                _values.Dispose();
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// <c>true</c> if the enumerator was successfully advanced to the next element; otherwise, <c>false</c>
            /// </returns>
            bool IEnumerator.MoveNext()
            {
                return this.MoveNext();
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            void IEnumerator.Reset()
            {
                this.Reset();
            }
        }
    }
}
