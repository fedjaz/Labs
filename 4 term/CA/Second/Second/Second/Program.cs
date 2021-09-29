using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    class Program
    {
        static void Main(string[] args)
        {
            Decimal32 a = new Decimal32(2.13f, true);

            Decimal32 b = new Decimal32(2.13f, true);
            Decimal32 res = (a + b);
            double f = res.ToFloat();
            Console.WriteLine(f);
        }
    }
}
