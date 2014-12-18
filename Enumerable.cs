using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabidWarren.Collections.Generic
{
    public static class Enumerable
    {
        public static Multimap<TKey, TElement> ToMultimap<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            var map = new Multimap<TKey, TElement>();

            foreach (var entry in source)
            {
                map.Add(keySelector(entry), elementSelector(entry));
            }

            return map;
        }
    }
}
