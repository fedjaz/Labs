using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите A и B в одну строку:");
            ulong a, b;
            (a, b) = GetAandB();
            if(a == 0)
            {
                Console.WriteLine(0);
                Console.ReadKey();
                return;
            }

            ulong prod = 0;
            ulong pow = 2;
            while (true)
            {
                ulong cnt = ((b / pow) - (a / pow));
                if (a % pow == 0)
                    cnt++;
                if (cnt == 0)
                    break;
                prod += cnt;
                pow *= 2;
            }
            Console.WriteLine(prod);
            Console.ReadKey();
        }
        static (ulong, ulong) GetAandB()
        {
            while (true)
            {
                ulong a, b;
                var s = Console.ReadLine().Split().Where(sb => sb != "").ToArray();
                if(ulong.TryParse(s[0], out a) && ulong.TryParse(s[1], out b) && b >= a)
                {
                    return (a, b);
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз!");
                }
            }
        } 
    }
}
