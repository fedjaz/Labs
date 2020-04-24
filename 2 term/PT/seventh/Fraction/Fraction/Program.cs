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
            List<Fraction> fractions = new List<Fraction>()
            {
                new Fraction(0.25),
                new Fraction(2),
                new Fraction(2, 4)
                //Creating fractions with different constructor types
            };

            fractions = fractions.Concat(new List<Fraction>()
            {
                Fraction.Parse("1/3"),
                Fraction.Parse("3"),
                Fraction.Parse("-2(1/4)"),
                Fraction.Parse("-0.75")
                //Parsing from different string formats
            }).ToList();

            Fraction testing;
            Console.WriteLine(Fraction.TryParse("Not a fraction", out testing));
            Console.WriteLine(Fraction.TryParse("27/54", out testing));
            Console.WriteLine(testing);
            //TryParse function

            fractions = fractions.Concat(new List<Fraction>()
            {
                new Fraction(3, 4) + new Fraction(2, 3),
                new Fraction(5, 6) - new Fraction(1, 6),
                new Fraction(1, 9) * new Fraction(9, 4),
                new Fraction(2, 3) / new Fraction(3, 2),
                //Math operations
            }).ToList();

            fractions.Add(new Fraction(7, 8).Clone() as Fraction);
            //Cloning fraction

            Console.WriteLine("____________________");
            Console.WriteLine(new Fraction(3, 4) > new Fraction(2, 3));
            Console.WriteLine(new Fraction(5, 6) <= new Fraction(1, 6));
            Console.WriteLine(new Fraction(1, 2) == new Fraction(2, 4));
            Console.WriteLine(new Fraction(3, 4) != new Fraction(6, 8));
            //Comparison operators

            Console.WriteLine("____________________");
            testing = new Fraction(-5, 2);
            Console.WriteLine("{0:F}", testing);
            Console.WriteLine("{0:IF}", testing);
            Console.WriteLine("{0:I}", testing);
            Console.WriteLine("{0:D}", testing);
            Console.WriteLine("{0:D5}", testing);
            //Converting to string in different formats and IFormattible implementation

            Console.WriteLine("____________________");
            Console.WriteLine((int)testing);
            Console.WriteLine((double)testing);
            Console.WriteLine(Convert.ToBoolean(testing));
            Console.WriteLine(Convert.ToString(testing));
            //Conversion operators and implementation of IConvertible

            fractions.Sort();
            fractions.Sort(new Fraction.DecreaseComparer());
            fractions.Sort((x, y) => y.CompareTo(x));
            //Sorting by using IComparable interface, default Decrease comparer and CompareTo
            //function

            Console.WriteLine("____________________");
            fractions.ForEach(x => Console.WriteLine("{0:IF}", x));

            Console.WriteLine("____________________");
            try
            {
                new Fraction(2, 0);
                //Division by zero exception
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }    

            try
            {
                Fraction.Parse("Not a fraction");
                //Trying to parse incorrect string exception
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                testing.ToString("Unsupported format");
                //Unsupported ToString() format exception
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
