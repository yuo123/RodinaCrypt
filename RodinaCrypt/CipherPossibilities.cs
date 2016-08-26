using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using MoreLinq;

namespace RodinaCrypt
{
    public class CipherPossibilities : IList<KeyValuePair<char, double>>
    {
        public int Code { get; set; }
        public List<KeyValuePair<char, double>> Probabilites { get; set; }

        public CipherPossibilities Self { get { return this; } }

        public KeyValuePair<char, double> DisplayPair { get { return Probabilites.Count > 0 ? Probabilites.First() : new KeyValuePair<char, double>(MainForm.UNKNOWN_CHAR, 0.0); } }

        public BindingList<KeyValuePair<char, double>> BindingList { get; set; }

        public int Count
        {
            get
            {
                return ((IList<KeyValuePair<char, double>>)this.Probabilites).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<KeyValuePair<char, double>>)this.Probabilites).IsReadOnly;
            }
        }

        public KeyValuePair<char, double> this[int index]
        {
            get
            {
                return ((IList<KeyValuePair<char, double>>)this.Probabilites)[index];
            }

            set
            {
                ((IList<KeyValuePair<char, double>>)this.Probabilites)[index] = value;
            }
        }

        public CipherPossibilities()
        {
            this.Probabilites = new List<KeyValuePair<char, double>>();
            BindingList = new BindingList<KeyValuePair<char, double>>(Probabilites);
            BindingList.ListChanged += ProbabilitiesChanged;
        }

        private void ProbabilitiesChanged(object sender, ListChangedEventArgs e)
        {
            Probabilites.Sort((a, b) => -a.Value.CompareTo(b.Value)); //minus sign for descending order
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
            Probabilites.Add(new KeyValuePair<char, double>(pair.Value, probability));
        }

        public CipherPair MostLikely()
        {
            if (Probabilites.Count == 0)
                return null;

            var likelyProb = Probabilites.First();
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

        private bool ContainsKey(char key)
        {
            return Probabilites.Any(p => p.Key == key);
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

        internal void Add(char c, double prob)
        {
            Probabilites.Add(new KeyValuePair<char, double>(c, prob));
        }

        public int IndexOf(KeyValuePair<char, double> item)
        {
            return ((IList<KeyValuePair<char, double>>)this.Probabilites).IndexOf(item);
        }

        public void Insert(int index, KeyValuePair<char, double> item)
        {
            ((IList<KeyValuePair<char, double>>)this.Probabilites).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<KeyValuePair<char, double>>)this.Probabilites).RemoveAt(index);
        }

        public void Add(KeyValuePair<char, double> item)
        {
            ((IList<KeyValuePair<char, double>>)this.Probabilites).Add(item);
        }

        public void Clear()
        {
            ((IList<KeyValuePair<char, double>>)this.Probabilites).Clear();
        }

        public bool Contains(KeyValuePair<char, double> item)
        {
            return ((IList<KeyValuePair<char, double>>)this.Probabilites).Contains(item);
        }

        public void CopyTo(KeyValuePair<char, double>[] array, int arrayIndex)
        {
            ((IList<KeyValuePair<char, double>>)this.Probabilites).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<char, double> item)
        {
            return ((IList<KeyValuePair<char, double>>)this.Probabilites).Remove(item);
        }

        public IEnumerator<KeyValuePair<char, double>> GetEnumerator()
        {
            return ((IList<KeyValuePair<char, double>>)this.Probabilites).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<KeyValuePair<char, double>>)this.Probabilites).GetEnumerator();
        }

        public double this[char key]
        {
            get
            {
                return Probabilites.Find(p => p.Key == key).Value;
            }
            set
            {
                Probabilites.RemoveAll(p => p.Key == key);
                Probabilites.Add(new KeyValuePair<char, double>(key, value));
            }
        }
    }
}
