using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MoreLinq;

namespace RodinaCrypt
{
    public class CipherPossibilities : IDictionary<char, double>
    {
        public int Code { get; set; }
        public Dictionary<char, double> Probabilites { get; set; }

        public CipherPossibilities()
        {
            this.Probabilites = new Dictionary<char, double>();
        }
        public CipherPossibilities(int code) : this()
        {
            this.Code = code;
        }
        /// <summary>
        /// Initializes a new instance of <see cref="CipherPossibilities"/> with a code and one possibility specified by a <see cref="CipherPair"/>
        /// </summary>
        public CipherPossibilities(CipherPair pair, double probability) : this(pair.Code)
        {
            Probabilites.Add(pair.Value, probability);
        }

        public CipherPair MostLikely()
        {
            if (Probabilites.Count == 0)
                return null;

            var likelyProb = Probabilites.MaxBy(p => p.Value);
            return new CipherPair(Code, likelyProb.Key);
        }

        public void Assimilate(CipherPossibilities subset)
        {
            foreach (KeyValuePair<char, double> poss in subset)
            {
                Assimilate(poss);
            }
        }

        public void Assimilate(KeyValuePair<char, double> poss)
        {
            if (!this.ContainsKey(poss.Key))
                this[poss.Key] = 0;

            this[poss.Key] = CombineProbabilities(this[poss.Key], poss.Value);
        }

        public void Assimilate(char c, double prob)
        {
            Assimilate(new KeyValuePair<char, double>(c, prob));
        }

        public static double CombineProbabilities(double a, double b)
        {
            // given probabilities of two independent events A and B, this is the formula for the probability of *either* A *or* B being true
            // notice that if A is 0, the output is B, and vice versa
            return a + b - (a * b);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + (Code & 0x00FFFFFF).ToString("X6");
        }

        #region IDictionary implementation through Probabilities

        public double this[char key]
        {
            get
            {
                return ((IDictionary<char, double>)this.Probabilites)[key];
            }

            set
            {
                ((IDictionary<char, double>)this.Probabilites)[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return ((IDictionary<char, double>)this.Probabilites).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IDictionary<char, double>)this.Probabilites).IsReadOnly;
            }
        }

        public ICollection<char> Keys
        {
            get
            {
                return ((IDictionary<char, double>)this.Probabilites).Keys;
            }
        }

        public ICollection<double> Values
        {
            get
            {
                return ((IDictionary<char, double>)this.Probabilites).Values;
            }
        }

        public void Add(KeyValuePair<char, double> item)
        {
            ((IDictionary<char, double>)this.Probabilites).Add(item);
        }

        public void Add(char key, double value)
        {
            ((IDictionary<char, double>)this.Probabilites).Add(key, value);
        }

        public void Clear()
        {
            ((IDictionary<char, double>)this.Probabilites).Clear();
        }

        public bool Contains(KeyValuePair<char, double> item)
        {
            return ((IDictionary<char, double>)this.Probabilites).Contains(item);
        }

        public bool ContainsKey(char key)
        {
            return ((IDictionary<char, double>)this.Probabilites).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<char, double>[] array, int arrayIndex)
        {
            ((IDictionary<char, double>)this.Probabilites).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<char, double>> GetEnumerator()
        {
            return ((IDictionary<char, double>)this.Probabilites).GetEnumerator();
        }

        public bool Remove(KeyValuePair<char, double> item)
        {
            return ((IDictionary<char, double>)this.Probabilites).Remove(item);
        }

        public bool Remove(char key)
        {
            return ((IDictionary<char, double>)this.Probabilites).Remove(key);
        }

        public bool TryGetValue(char key, out double value)
        {
            return ((IDictionary<char, double>)this.Probabilites).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<char, double>)this.Probabilites).GetEnumerator();
        }

        #endregion
    }
}
