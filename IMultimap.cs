using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabidWarren.Collections.Generic
{
    public interface IMultimap<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultimap<TKey, TValue>"./>
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Determines whether the <see cref="IMultimap<TKey, TValue>"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IMultimap<TKey, TValue>"/>.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="IMultimap<TKey, TValue>"/> contains an element with the key;
        /// otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Removes the element with the specified key and value from the <see cref="IMultimap<TKey, TValue>"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The value of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if the <paramref name="key"/> and matching <paramref name="value"/> were not found
        /// in the original <see cref="IMultimap<TKey, TValue>"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is <c>null</c>.</exception>
        bool Remove(TKey key, TValue value);

        /// <summary>
        /// Removes all elements with the specified key from the <see cref="IMultimap<TKey, TValue>"/>.
        /// </summary>
        /// <param name="key">The key of the elements to remove.</param>
        /// <returns><c>true</c> if the elements are successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if the <paramref name="key"/> was not found in the original
        /// <see cref="IMultimap<TKey, TValue>"/>.</returns>
        bool RemoveAll(TKey key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose values should be gotten.</param>
        /// <param name="values">When this method returns, the values associated with the specified key, if they key
        /// is found; otherwise, at empty collection is returned. The parameter should be passed uninitialized. It
        /// will not be updated, it will be replaced.</param>
        /// <returns><c>true</c> if the <see cref="IMultimap<TKey, TValue>"/> object contains any elements with the
        /// specfied key; otherwise, <c>false</c>.</returns>
        bool TryGetValues(TKey key, out ICollection<TValue> values);
    }
}
