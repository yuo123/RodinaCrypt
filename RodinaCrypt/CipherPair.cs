using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RodinaCrypt
{
    public class CipherPair
    {
        public int Code { get; set; }
        public char Value { get; set; }
        public CipherPair() { }
        public CipherPair(int code, char value) : this()
        {
            Code = code;
            Value = value;
        }
        public CipherPair(int code) : this(code, MainForm.UNKNOWN_CHAR) { }
        public CipherPair(KeyValuePair<int, char> item) : this(item.Key, item.Value) { }

        public KeyValuePair<int, char> ToKeyValuePair()
        {
            return new KeyValuePair<int, char>(Code, Value);
        }

        public override string ToString()
        {
            return string.Format("CipherPair: [{0:X6}, '{1}']", this.Code, this.Value);
        }

        public bool EncodingEquals(CipherPair other)
        {
            return other != null && other.Code == this.Code && other.Value == this.Value;
        }
    }

    public class EncodingComparer : IEqualityComparer<CipherPair>
    {
        public bool Equals(CipherPair x, CipherPair y)
        {
            return x.EncodingEquals(y);
        }

        public int GetHashCode(CipherPair obj)
        {
            return obj.Code.GetHashCode() + obj.Value.GetHashCode();
        }
    }
}
