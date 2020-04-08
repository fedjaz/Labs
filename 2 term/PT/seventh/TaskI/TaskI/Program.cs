using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskI
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1, s2;
            s1 = Console.ReadLine();
            s2 = Console.ReadLine();
            Check(s1, s2, 0, "");
        }

        static void Check(string s1, string s2, int depth, string oldstr)
        {
            if(depth > 2)
                return;
            for(int i = 0; i < s1.Length; i++)
            {
                if(s1[i] != s2[i])
                {
                    Check(s1.Substring(i), s2.Substring(i + 1), depth + 1, oldstr + s1.Substring(0, i) + s2[i]);
                    Check(s1.Substring(i + 1), s2.Substring(i), depth + 1, oldstr + s2.Substring(0, i) + s1[i]);
                    break;
                }
            }
        }
    }

    
}
