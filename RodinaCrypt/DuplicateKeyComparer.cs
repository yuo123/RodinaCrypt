using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodinaCrypt
{
    //taken from http://stackoverflow.com/a/21886340
    public class DescendingDuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
    {
        #region IComparer<TKey> Members

        public int Compare(TKey x, TKey y)
        {
            int result = -x.CompareTo(y); //the minus reverses the order

            if (result == 0)
                return 1;   // Handle equality as being greater
            else
                return result;
        }

        #endregion
    }
}
