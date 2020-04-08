using System;
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
            Fraction f1 = new Fraction(-1, 2);
            Fraction f2 = new Fraction(1, 2);
            Console.WriteLine(f1.ToString("D10"));
        }
    }
}
