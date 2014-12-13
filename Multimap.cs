namespace RabidWarren.Collections.Generic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Collections;

    public class Multimap<TKey, TValue> : IMultimap<TKey, TValue>
    {
		private Dictionary<TKey, List<TValue>> _dictionary = new Dictionary<TKey, List<TValue>>();

		/// <summary>
		/// Adds an element with the provided key and value to the
		/// <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"./>
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
		/// Determines whether the <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/> contains an
		/// element with the specified key.
		/// </summary>
		/// <param name="key">The key to locate in the
		/// <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/>.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/> contains an element
		/// with the key; otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		public bool ContainsKey(TKey key)
		{
            VerifyKey(key);

            return _dictionary.ContainsKey(key);
		}

		/// <summary>
		/// Removes the element with the specified key and value from the
		/// <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/>.
		/// </summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <param name="value">The value of the element to remove.</param>
		/// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also
		/// returns <c>false</c> if the <paramref name="key"/> and matching <paramref name="value"/> were not found
		/// in the original <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
		public bool Remove(TKey key, TValue value)
		{
            VerifyKey(key);
            List<TValue> entries;

            if (!_dictionary.TryGetValue(key, out entries))
            {
                return false;
            }

            return entries.Remove(value);
		}

		/// <summary>
		/// Removes all elements with the specified key from the
		/// <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/>.
		/// </summary>
		/// <param name="key">The key of the elements to remove.</param>
		/// <returns><c>true</c> if the elements are successfully removed; otherwise, <c>false</c>. This method also
		/// returns <c>false</c> if the <paramref name="key"/> was not found in the original
        /// <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/>.</returns>
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
		/// <returns><c>true</c> if the <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/> object
		/// contains any elements with the specfied key; otherwise, <c>false</c>.</returns>
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
        /// The key-value pair to add to the <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>.
        /// </param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all items from the <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/> contains
        /// a specific key-value pair.
        /// </summary>
        /// <param name="item">Item.</param>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            List<TValue> entries;

            if (_dictionary.TryGetValue(item.Key, out entries))
            {
                return entries.Contains(item.Value);
            }

            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/> to a
        /// <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements
        /// copied from <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>. The
        /// <see cref="System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array "/>at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.
		/// </exception>
        /// <exception cref="System.ArgumentException">The number of elements in the source
        /// <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/> is greater than the available space
        /// from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("arrayIndex");
            
            var count = Count;
            if (count > 0 && arrayIndex + count >= array.Length)
            {
                var message = String.Format(
                    "Copying {0} elements into the {1} element array beginning at {2} would exceed its length.",
                    count, array.Length, arrayIndex);

                throw new ArgumentException(message);
            }

            foreach (var keyValuePair in this)
            {
                array[arrayIndex] = keyValuePair;
                arrayIndex++;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>.
        /// </summary>
        /// <value>The number of elements contained in the
        /// <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>.</value>
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
        /// Removes the first occurrence of a specific object from the
        /// <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>.
        /// </summary>
        /// <param name="item">The object to remove from the
        /// <see cref="RabidWarren.Collections.Generic.MultiMap<TKey, TValue>"/>.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also
		/// returns <c>false</c> if the <paramref name="key"/> and matching <paramref name="value"/> were not found
		/// in the original <see cref="RabidWarren.Collections.Generic.Multimap<TKey, TValue>"/> or if the key is
		/// null.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return (item.Key == null) ? false : Remove(item.Key, item.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(_dictionary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static void VerifyKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
        }

        class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDictionaryEnumerator, IEnumerator
        {
            private Dictionary<TKey, List<TValue>> _dictionary;
            private Dictionary<TKey, List<TValue>>.Enumerator _keys;
            private List<TValue>.Enumerator _values;

            public Enumerator(Dictionary<TKey, List<TValue>> dictionary)
            {
                _dictionary = dictionary;
                Reset();
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(_keys.Current.Key, _values.Current);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return new KeyValuePair<TKey, TValue>(_keys.Current.Key, _values.Current);
                }
            }

            public bool MoveNext()
            {
                if (_values.MoveNext())
                    return true;

                if (!_keys.MoveNext())
                    return false;
               
                _values = _keys.Current.Value.GetEnumerator();

                // Prime the new _values enumerator so that Entry, will return a valid key-value pair.
                return _values.MoveNext();
            }

            public void Reset()
            {
                _keys = _dictionary.GetEnumerator();
                _values = new List<TValue>.Enumerator();
            }

            public void Dispose()
            {
                _keys.Dispose();
                _values.Dispose();
            }


            bool IEnumerator.MoveNext()
            {
                return this.MoveNext();
            }

            void IEnumerator.Reset()
            {
                this.Reset();
            }

            public object Key
            {
                get { return _keys.Current.Key; }
            }

            public object Value
            {
                get { return _values.Current; }
            }

            public DictionaryEntry Entry
            {
                get { return new DictionaryEntry(_keys.Current.Key, _values.Current); }
            }
        }
    }
}
