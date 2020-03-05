using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            s = s.Where(substr => substr != "").Reverse().ToArray();
            Console.WriteLine(string.Join(" ", s));
            Console.ReadKey();
        }
    }
}
