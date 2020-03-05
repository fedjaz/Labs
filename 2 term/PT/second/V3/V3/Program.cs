using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V3
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            var arr = s.Split(' ').Select(sb => sb.ToUpper()).Where(sub => sub != "" && IsHex(sub));
            arr = arr.Select(sb => int.Parse(sb, System.Globalization.NumberStyles.HexNumber).ToString());
            arr.ToList().ForEach(sb => Console.Write(sb + " "));
            Console.WriteLine();
            Console.ReadKey();
        }

        static bool IsHex(string s)
        {
            foreach(char c in s)
            {
                if(!((c >= 'A' && c <= 'F') || (c >= '0' && c <= '9'))){
                    return false;
                }
            }
            return true;
        }
    }
}
