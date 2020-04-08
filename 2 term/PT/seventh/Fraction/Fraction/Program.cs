using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            Fraction fraction;
            Fraction.TryParse(s, out fraction);
            Console.WriteLine("{0:F}", fraction);
        }
    }
}
