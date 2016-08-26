using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodinaCrypt
{
    static class Extensions
    {
        public static KeyValuePair<TValue,TKey> Swap<TKey,TValue>(this KeyValuePair<TKey,TValue> pair)
        {
            return new KeyValuePair<TValue, TKey>(pair.Value, pair.Key);
        }
    }
}
