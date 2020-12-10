using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    static class Extensions
    {
        public static int Length(this IEnumerable ie)
        {
            int length = 0;
            foreach(object item in ie)
            {
                length++;
            }
            return length;
        }
    }
}
