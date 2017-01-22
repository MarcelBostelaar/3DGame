using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GenWrapp<T>
    {
        public GenWrapp(T value)
        {
            Value = value;
        }
        public T Value;
    }
}
