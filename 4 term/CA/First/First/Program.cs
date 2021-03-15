using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    class Program
    {
        static void Main()
        {
            Integer a = new Integer(99, 8, true);
            Integer b = new Integer(5, 8, true);

            int c = int.MaxValue;
            int d = 150;
            int e = c + d;
            unchecked
            {
                Console.WriteLine(a / b);
            }
        }
    }
}
