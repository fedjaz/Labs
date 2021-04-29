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
            Decimal32 a = new Decimal32(-1f, true);
            Decimal32 b = new Decimal32(-3f, true);
            Console.WriteLine($"{(a / b).ToFloat():0.####}");
        }
    }
}
