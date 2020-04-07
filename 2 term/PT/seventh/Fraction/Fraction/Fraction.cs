using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    class Fraction : IComparable<Fraction>
    {
        public int Numerator { get => numerator; set => SetNumerator(value); }
        int numerator;
        public int Denominator { get => denominator; set => SetDenominator(value); }
        int denominator;

        void SetNumerator(int numerator)
        {
            if(denominator != 0 && numerator != 0)
            {
                int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
                numerator /= gcd;
                denominator /= gcd;
            }
            this.numerator = numerator;
        }

        void SetDenominator(int denominator)
        {
            if(denominator == 0)
            {
                throw new ArgumentException("Denominator can't be zero.");
            }

            if(denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }

            if(numerator != 0)
            {
                int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
                numerator /= gcd;
                denominator /= gcd;
            }
            this.denominator = denominator;
        }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public static bool operator >(Fraction a, Fraction b) =>
            a.CompareTo(b) == 1;

        public static bool operator <(Fraction a, Fraction b) =>
            a.CompareTo(b) == -1;

        public static bool operator >=(Fraction a, Fraction b) =>
            a.CompareTo(b) != -1;

        public static bool operator <=(Fraction a, Fraction b) =>
            a.CompareTo(b) != 1;

        public static bool operator ==(Fraction a, Fraction b) =>
            a.CompareTo(b) == 0;

        public static bool operator !=(Fraction a, Fraction b) =>
            a.CompareTo(b) != 0;

        public static Fraction operator +(Fraction a, Fraction b)
        {
            int lcm = a.Denominator * b.Denominator /
                      GCD(Math.Abs(a.Denominator), Math.Abs(b.Denominator));

            int n1 = a.Numerator * (lcm / a.Denominator);
            int n2 = b.Numerator * (lcm / b.Denominator);
            return new Fraction(n1 + n2, lcm);
        }

        public static Fraction operator -(Fraction a, Fraction b) =>
            a + b * -1;

        public static Fraction operator *(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static Fraction operator *(Fraction a, int b) =>
            new Fraction(a.Numerator * b, a.Denominator);

        public static Fraction operator /(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator); 

        public int CompareTo(Fraction fraction)
        {
            long lcm = (long)denominator * fraction.Denominator /
                       GCD(Math.Abs(denominator), Math.Abs(fraction.Denominator));

            long n1 = numerator * (lcm / denominator);
            long n2 = fraction.Numerator * (lcm / fraction.Denominator);
            if(n1 > n2)
                return 1;
            else if(n1 < n2)
                return -1;
            return 0;
        }

        static int GCD(int a, int b)
        {
            if(a > b)
            {
                (a, b) = (b, a);
            }
            while(b != 0)
            {
                a %= b;
                (a, b) = (b, a);
            }
            return a;
        }

        public override bool Equals(object obj)
        {
            return obj is Fraction fraction &&
                   numerator == fraction.numerator &&
                   denominator == fraction.denominator;
        }

        public override int GetHashCode()
        {
            int hashCode = -671859081;
            hashCode = hashCode * -1521134295 + numerator.GetHashCode();
            hashCode = hashCode * -1521134295 + denominator.GetHashCode();
            return hashCode;
        }
    }
}
