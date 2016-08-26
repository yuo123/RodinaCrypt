using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RodinaCrypt
{
    public class CipherPredictionDictionary : List<CipherPossibilities>
    {
        public CipherPredictionDictionary() : base() { }
        public CipherPredictionDictionary(int capacity) : base(capacity) { }

        /// <summary>
        /// Add an empty <see cref="CipherPossibilities"/> to this dictionary for the specified code
        /// </summary>
        /// <returns>The newly-created empty <see cref="CipherPossibilities"/></returns>
        public CipherPossibilities Add(int code)
        {
            var newPoss = new CipherPossibilities(code);
            this.Add(newPoss);
            return newPoss;
        }

        public CipherDictionary MostLikely()
        {
            var ret = new CipherDictionary(this.Count);
            foreach (CipherPossibilities poss in this)
            {
                CipherPair ml = poss.MostLikely();
                if (ml != null)
                    ret.Add(ml);
            }
            return ret;
        }
        
        public CipherPossibilities GetPossibilitiesForCode(int code)
        {
            return this.Find(p => p.Code == code);
        }

        public static CipherPredictionDictionary AggregateProbabilities(IEnumerable<CipherPredictionDictionary> dicts, IEnumerable<int> codelist)
        {
            CipherPredictionDictionary final = new CipherPredictionDictionary(codelist.Count());

            foreach (int code in codelist) //for all possible codes
            {
                //initialize a new entry for this code
                CipherPossibilities curP = final.Add(code);
                foreach (CipherPredictionDictionary dict in dicts) //for all available dictionaries
                {
                    CipherPossibilities p = dict.GetPossibilitiesForCode(code); //check if it contains possibilities for the code we're looking for
                    if (p != null)
                        curP.Assimilate(p); //if so, assimilate it
                }
            }

            return final;
        }

        #region Prediction Methods

        public static readonly char[] singleFreqOrder = { ' ', 'e', 't', 'a', 'o', 'i', 'n', 's', 'r', 'h', 'l', 'd', 'c', 'u', 'm', 'f', 'p', 'g', 'w', 'y', 'b', 'v', 'k', 'x', 'j', 'q', 'z' };

        public static CipherPredictionDictionary SingleFrequencyPrediction(int[] data, CipherPair eof = null)
        {
            const double PROBABILITY_COEFFICIENT = 0.2;
            const int PRED_DISTANCE = 4;

            int[] freqs = CalcSingleFreqs(data, eof);
            var dict = new CipherPredictionDictionary(freqs.Length + 1);
            for (int i = 0; i < freqs.Length; i++)
            {
                CipherPossibilities poss = dict.Add(freqs[i]);
                //loop over indexes from (i - PRED_DISTANCE) to (i + PRED_DISTANCE), within the bounds of the array
                for (int j = Math.Max(i - PRED_DISTANCE, 0); j < Math.Min(i + PRED_DISTANCE + 1, singleFreqOrder.Length); j++)
                {
                    poss.Add(singleFreqOrder[j], (PRED_DISTANCE - Math.Abs(i - j)) / (double)PRED_DISTANCE * PROBABILITY_COEFFICIENT);
                }
            }
            dict.Insert(dict.Count, new CipherPossibilities(eof, 1.0));

            return dict;
        }

        public static readonly string[] pairFreqOrder = { "th", "he", "an", "in", "er", "on", "re", "ed", "nd", "ha", "at", "en", "es", "of", "nt", "\r\n", "ea", "ti", "to", "io", "le", "is", "ou", "ar", "as", "de", "rt", "ve" };

        public static CipherPredictionDictionary PairFrequencyPrediction(int[] data, IEnumerable<int> codelist, CipherPair eof = null)
        {
            const double PROBABILITY_COEFFICIENT = 0.3;
            const int PRED_DISTANCE = 6;

            ulong[] freqs = CalcPairFreqs(data, codelist.ToArray(), eof);
            var dict = new CipherPredictionDictionary(freqs.Length + 1);
            for (int i = 0; i < freqs.Length; i++)
            {
                CipherPossibilities poss1 = dict.Add((int)(freqs[i] >> 32)); //first char
                CipherPossibilities poss2 = dict.Add((int)(freqs[i] & uint.MaxValue)); //second char
                //loop over indexes from (i - PRED_DISTANCE) to (i + PRED_DISTANCE), within the bounds of the array
                for (int j = Math.Max(i - PRED_DISTANCE, 0); j < Math.Min(i + PRED_DISTANCE + 1, pairFreqOrder.Length); j++)
                {
                    double prob = (PRED_DISTANCE - Math.Abs(i - j)) / (double)PRED_DISTANCE * PROBABILITY_COEFFICIENT;
                    poss1.Assimilate(pairFreqOrder[j][0], prob);
                    poss2.Assimilate(pairFreqOrder[j][1], prob);
                }
            }
            return dict;
        }

        public static readonly char[] firstFreqOrder = { 't', 'o', 'a', 'w', 'b', 'c', 'd', 's', 'f', 'm', 'r', 'h', 'i', 'y', 'e', 'g', 'l', 'n', 'o', 'u', 'j', 'k' };

        public static CipherPredictionDictionary FirstLetterPrediction(int[] data, int spaceCode)
        {
            const double PROBABILITY_COEFFICIENT = 0.6;
            const int PRED_DISTANCE = 3;

            int[] freqs = CalcFirstFreqs(data, spaceCode);
            var dict = new CipherPredictionDictionary(freqs.Length + 1);
            for (int i = 0; i < freqs.Length; i++)
            {
                CipherPossibilities poss = dict.Add(freqs[i]);
                //loop over indexes from (i - PRED_DISTANCE) to (i + PRED_DISTANCE), within the bounds of the array
                for (int j = Math.Max(i - PRED_DISTANCE, 0); j < Math.Min(i + PRED_DISTANCE + 1, firstFreqOrder.Length); j++)
                {
                    poss.Add(singleFreqOrder[j], (PRED_DISTANCE - Math.Abs(i - j)) / (double)PRED_DISTANCE * PROBABILITY_COEFFICIENT);
                }
            }

            return dict;
        }

        public static int[] CalcSingleFreqs(int[] data, CipherPair eof = null)
        {
            Dictionary<int, int> freqs = new Dictionary<int, int>();
            foreach (int point in data)
            {
                if (eof != null && point == eof.Code)
                    break;
                int freq;
                if (freqs.TryGetValue(point, out freq))
                    freqs[point]++;
                else
                    freqs.Add(point, 1);
            }
            return freqs.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).ToArray();
        }

        public static ulong[] CalcPairFreqs(int[] data, int[] codelist, CipherPair eof = null)
        {
            //convert the data into a string, so we can use regex on it
            char[] ctext = new char[data.Length * 4];
            Buffer.BlockCopy(data, 0, ctext, 0, data.Length * 4);
            string text = new string(ctext);

            var count = new SortedList<int, ulong>(codelist.Length * codelist.Length, new DescendingDuplicateKeyComparer<int>());
            for (int i = 0; i < codelist.Length; i++)
            {
                for (int j = 0; j < codelist.Length; j++)
                {
                    count.Add(Regex.Matches(text, string.Concat(codelist[i], codelist[j])).Count, ((ulong)i << 32) | (uint)j);
                }
            }
            return count.Select(p => p.Value).ToArray();
        }

        public static int[] CalcFirstFreqs(int[] data, int spaceCode)
        {
            Dictionary<int, int> rfreqs = new Dictionary<int, int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != spaceCode && (i == 0 || data[i - 1] == spaceCode))
                    if (rfreqs.ContainsKey(data[i]))
                        rfreqs[data[i]]++;
                    else
                        rfreqs[data[i]] = 1;
            }
            return rfreqs.OrderByDescending(p => p.Value).Select(p => p.Key).ToArray();
        }

        #endregion
    }
}
