// -----------------------------------------------------------------------
//  <copyright file="ExtensionMethods.cs" company="Ron Parker">
//   Copyright 2014, 2015 Ron Parker
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
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts the source to an <see cref="Generic.Multimap{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <returns>The <see cref="Generic.Multimap{TKey, TValue}"/>.</returns>
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
