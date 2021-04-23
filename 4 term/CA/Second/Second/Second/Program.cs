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
            Decimal32 decimal32 = new Decimal32(-2754.8888888f);
            Console.WriteLine(decimal32.ToFloat());
        }
    }
}
