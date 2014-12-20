// -----------------------------------------------------------------------
//  <copyright file="Enumerable.cs" company="Ron Parker">
//   Copyright 2014 Ron Parker
//  </copyright>
//  <summary>
//   Provides an extension method for converting IEnumerables to Multimaps.
//  </summary>
// -----------------------------------------------------------------------

namespace RabidWarren.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains extension methods for <see cref="System.Collections.Generic.IEnumerable{TSource}"/>.
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// Converts the source to an <see cref="RabidWarren.Collections.Generic.Multimap{TSource, TKey, TValue}"/>.
        /// </summary>
        /// <returns>The <see cref="RabidWarren.Collections.Generic.Multimap{TSource, TKey, TValue}"/>.</returns>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The the value type.</typeparam>
        public static Multimap<TKey, TValue> ToMultimap<TSource, TKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            var map = new Multimap<TKey, TValue>();

            foreach (var entry in source)
            {
                map.Add(keySelector(entry), valueSelector(entry));
            }

            return map;
        }
    }
}
