using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RodinaCrypt
{
    public class ValueSortedDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TValue : IComparable
    {
        private SortedList<TValue, TKey> m_internal;

        public ValueSortedDictionary()
        {
            m_internal = new SortedList<TValue, TKey>(new DescendingDuplicateKeyComparer<TValue>());
        }

        public TValue this[TKey key]
        {
            get
            {
                return m_internal.First(p => p.Value.Equals(key)).Key;
            }

            set
            {
                this.Remove(key);
                m_internal[value] = key;
            }
        }

        public int Count
        {
            get
            {
                return m_internal.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return m_internal.Values;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return m_internal.Keys;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            m_internal.Add(item.Value, item.Key);
        }

        public void Add(TKey key, TValue value)
        {
            m_internal.Add(value, key);
        }

        public void Clear()
        {
            m_internal.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return m_internal.ContainsValue(item.Key);
        }

        public bool ContainsKey(TKey key)
        {
            return m_internal.ContainsValue(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int i = arrayIndex;
            foreach (var item in m_internal)
            {
                array[i] = item.Swap();
                i++;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in m_internal)
            {
                yield return item.Swap();
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

        public bool Remove(TKey key)
        {
            int i = 0;
            foreach (var item in m_internal)
            {
                if (item.Value.Equals(key))
                {
                    m_internal.RemoveAt(i);
                    return true;
                }
                i++;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            foreach (var item in m_internal)
            {
                if (item.Value.Equals(key))
                {
                    value = item.Key;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in this)
            {
                yield return item;
            }
        }

        public void RemoveAt(int index)
        {
            m_internal.RemoveAt(index);
        }
    }
}
