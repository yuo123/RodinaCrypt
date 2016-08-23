using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace RodinaCrypt
{
    public class CipherDictionary : List<CipherPair>, IDictionary<int, char>, IList<CipherPair>, IEnumerable<CipherPair>
    {
        private BindingList<CipherPair> m_bindingList;

        public BindingList<CipherPair> BindingList
        {
            //return the saved binding list, or initialize a new one if it's null
            get { return (m_bindingList = (m_bindingList ?? new BindingList<CipherPair>(this))); }
        }

        public CipherPair GetPairForCode(int code)
        {
            return Find(p => p.Code == code);
        }

        public CipherDictionary() : base() { }

        public CipherDictionary(int capacity) : base(capacity) { }

        public void AddMissingValues(IEnumerable<int> codelist)
        {
            foreach (int code in codelist)
            {
                if (!this.ContainsKey(code))
                    this.Add(new CipherPair(code, MainForm.UNKNOWN_CHAR));
            }
        }
        
        #region IDictionary Implementation

        char IDictionary<int, char>.this[int key]
        {
            get
            {
                return this.Find(p => p.Code == key).Value;
            }

            set
            {
                this.Find(p => p.Code == key).Value = value;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<int> Keys
        {
            get { return Enumerable.Select<CipherPair, int>(this, p => p.Code).ToList(); }
        }

        public ICollection<char> Values
        {
            get { return Enumerable.Select<CipherPair, char>(this, p => p.Value).ToList(); }
        }

        public void Add(KeyValuePair<int, char> item)
        {
            base.Add(new CipherPair(item));
        }

        public void Add(int key, char value)
        {
            this.Add(new CipherPair(key, value));
        }

        public bool Contains(KeyValuePair<int, char> item)
        {
            return base.Contains(new CipherPair(item));
        }

        public bool ContainsKey(int key)
        {
            return Enumerable.Any<CipherPair>(this, p => p.Code == key);
        }

        public void CopyTo(KeyValuePair<int, char>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = this[i].ToKeyValuePair();
            }
        }

        public bool Remove(KeyValuePair<int, char> item)
        {
            return base.Remove(new CipherPair(item));
        }

        public bool Remove(int key)
        {
            return Remove(Find(p => p.Code == key));
        }

        public bool TryGetValue(int key, out char value)
        {
            CipherPair pair = Find(p => p.Code == key);
            if (pair == null)
            {
                value = '\0';
                return false;
            }
            value = pair.Value;
            return true;
        }

        IEnumerator<KeyValuePair<int, char>> IEnumerable<KeyValuePair<int, char>>.GetEnumerator()
        {
            return Enumerable.Select<CipherPair, KeyValuePair<int, char>>(this, p => p.ToKeyValuePair()).GetEnumerator();
        }

        #endregion
    }
}
/*
 * 1 2 3 4 1 2 3 4 1 2 3 4 1 2 3 4 1 2 3 4 1 2 3 4 1 2 3 4 1 2 3 4 - 0.25
 * 1 1 1 1 2 2 2 2 3 3 3 3 4 4 4 4 1 1 1 1 2 2 2 2 3 3 3 3 4 4 4 4 - 0.25
 * 1 1 1 1 1 1 1 1 2 2 2 2 2 2 2 2 3 3 3 3 3 3 3 3 4 4 4 4 4 4 4 4 - 0.25
 * 
 * 17/32
 */
