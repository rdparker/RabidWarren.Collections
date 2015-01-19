namespace RabidWarren.Collections.Tests
{
    using System;
    using Generic;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics.CodeAnalysis;
    using System.Collections;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class MultimapTests
    {
        Multimap<int, string> map;

        KeyValuePair<int, string>[] sampleData = new[]
        {
            new KeyValuePair<int, string>(1, "one"),
            new KeyValuePair<int, string>(2, "two"),
            new KeyValuePair<int, string>(2, "too"),
            new KeyValuePair<int, string>(3, "three"),
        };

        [SetUp]
        public void CreateMap()
        {
            map = new Multimap<int, string>();
        }

        [Test]
        public void Add()
        {
            AddEntries(1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            new Multimap<string, int>().Add(null, 0);
        }

        [Test]
        public void ContainsKey()
        {
            AddEntries(3);

            Assert.True(map.ContainsKey(2));
        }

        [Test]
        public void DoesNotContainKey()
        {
            AddEntries(3);

            Assert.False(map.ContainsKey(3));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsKeyNull()
        {
            new Multimap<string, int>().ContainsKey(null);
        }

        [Test]
        public void Count()
        {
            AddEntries(1);

            Assert.AreEqual(map.Count, 1);
        }

        [Test]
        public void RemoveSucceeds()
        {
            AddEntries(1);
            Assert.True(map.Remove(1, "one"));
        }

        [Test]
        public void RemoveFails()
        {
            AddEntries(1);
            Assert.False(map.Remove(2, "two"));
        }

        [Test]
        public void RemoveLeaveDuplicate()
        {
            AddEntries(1);
            AddEntries(1);
            Assert.AreEqual(map.Count, 2);

            Assert.True(map.Remove(1, "one"));
            Assert.True(map.Contains(sampleData[0]));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveNull()
        {
            new Multimap<string, int>().Remove(null, 1);
        }

        [Test]
        public void RemoveAll()
        {
            AddEntries(4);
            Assert.AreEqual(map.Count, 4);

            Assert.True(map.RemoveAll(2));
            Assert.AreEqual(map.Count, 2);
            Assert.True(map.Contains(sampleData[0]));
        }

        [Test]
        public void RemoveAllFails()
        {
            AddEntries(4);
            Assert.False(map.RemoveAll(4));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveAllNull()
        {
            new Multimap<string, int>().Remove(null, 1);
        }

        [Test]
        public void TryGetValues()
        {
            ICollection<string> values;

            AddEntries(4);
            var result = map.TryGetValues(2, out values);
            Assert.True(result);
            Assert.True(values.Contains("two"));
            Assert.True(values.Contains("too"));
            Assert.AreEqual(values.Count, 2);
        }

        [Test]
        public void TryGetFails()
        {
            ICollection<string> values;

            AddEntries(4);
            var result = map.TryGetValues(5, out values);
            Assert.False(result);
            Assert.Null(values);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetNull()
        {
            ICollection<int> values;

            var emptyMap = new Multimap<string, int>();

            emptyMap.TryGetValues(null, out values);
        }

        [Test]
        public void IsReadonly()
        {
            var map = new Multimap<int, string>();

            Assert.False(map.IsReadOnly);
        }

        [Test]
        public void AddPair()
        {
            map.Add(sampleData[0]);
            Assert.AreEqual(map.Count, 1);
        }

        [Test]
        public void Clear()
        {
            AddEntries(4);
            map.Clear();
            Assert.AreEqual(map.Count, 0);
        }

        [Test]
        public void Contains()
        {
            AddEntries(1);
            Assert.True(map.Contains(sampleData[0]));
        }

        [Test]
        public void ContainsFalse()
        {
            AddEntries(1);
            Assert.False(map.Contains(sampleData[1]));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyToNull()
        {
            KeyValuePair<int, string>[] array = null;

            map.CopyTo(array, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToOutOfRange()
        {
            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[4];

            map.CopyTo(array, -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToOutOfRoom()
        {
            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[1];

            AddEntries(2);
            map.CopyTo(array, 0);
        }

        [Test]
        public void CopyTo()
        {
            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[4];

            AddEntries(3);
            map.CopyTo(array, 0);
            Assert.AreEqual(array.Take(3), sampleData.Take(3));
        }

        /// <summary>
        /// This tests the edge condition, where the amount of data being copied matches the length of
        /// the array.  Initially it caused an exception, where it should not have.
        /// </summary>
        [Test]
        public void CopyToSameSize()
        {
            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[4];

            AddEntries(4);
            map.CopyTo(array, 0);
            Assert.AreEqual(array, sampleData);
        }

        /// <summary>
        /// This completes code coverage to the case in CopyTo where an if exits early.
        /// </summary>
        [Test]
        public void CopyToEmpty()
        {
            KeyValuePair<int, string>[] array = new KeyValuePair<int, string>[4];

            map.CopyTo(array, 0);
            foreach (var entry in array)
            {
                Assert.AreEqual(entry, default(KeyValuePair<int, string>));
            }
        }

        [Test]
        public void RemoveKeyPair()
        {
            AddEntries(1);

            Assert.True(map.Remove(sampleData[0]));
        }

        [Test]
        public void RemoveKeyPairFails()
        {
            AddEntries(1);

            Assert.False(map.Remove(sampleData[2]));
        }

        [Test]
        public void IEnumerable()
        {
            AddEntries(4);
            var enumerable = map as IEnumerable;

            int count = 0;
            foreach (var item in enumerable)
            {
                count++;
            }

            Assert.AreEqual(count, 4);
        }

        [Test]
        public void EnumeratorKey()
        {
            AddEntries(1);

            var enumerator = map.GetEnumerator() as Multimap<int, string>.Enumerator;
            enumerator.MoveNext();

            Assert.AreEqual(enumerator.Key, sampleData[0].Key);
        }

        [Test]
        public void EnumeratorValue()
        {
            AddEntries(1);

            var enumerator = map.GetEnumerator() as Multimap<int, string>.Enumerator;
            enumerator.MoveNext();

            Assert.AreEqual(enumerator.Value, sampleData[0].Value);
        }

        [Test]
        public void EnumeratorEntry()
        {
            AddEntries(1);

            var enumerator = map.GetEnumerator() as Multimap<int, string>.Enumerator;
            enumerator.MoveNext();

            Assert.AreEqual(enumerator.Entry, new DictionaryEntry(sampleData[0].Key, sampleData[0].Value));
        }

        [Test]
        public void EnumeratorReset()
        {
            AddEntries(3);

            var enumerator = map.GetEnumerator();
            enumerator.MoveNext();
            enumerator.Reset();
            enumerator.MoveNext();
            enumerator.MoveNext();

            Assert.AreEqual(enumerator.Current.Key, sampleData[1].Key);
        }

        [Test]
        public void ToMultiMap()
        {
            AddEntries(4);
            var mm = sampleData.ToMultimap(x => x.Key, x => x.Value);

            Assert.AreEqual(map, mm);
        }

        void AddEntries(int n)
        {
            for (int i = 0; i < n; i++)
            {
                map.Add(sampleData[i].Key, sampleData[i].Value);
            }
        }
    }
}
