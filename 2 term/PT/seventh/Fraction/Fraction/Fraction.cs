using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fraction
{
    class Fraction : IComparable<Fraction>, IFormattable, IConvertible
    {
        public long Numerator { get => numerator; set => SetNumerator(value); }
        long numerator;
        public long Denominator { get => denominator; set => SetDenominator(value); }
        long denominator;

        public Fraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public Fraction(long numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }

        public Fraction(double fraction)
        {
            Fraction f = Parse(fraction.ToString());
            Numerator = f.Numerator;
            Denominator = f.Denominator;
        }

        void SetNumerator(long numerator)
        {
            if(denominator != 0 && numerator != 0)
            {
                long gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
                numerator /= gcd;
                denominator /= gcd;
            }
            this.numerator = numerator;
        }

        void SetDenominator(long denominator)
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
                long gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
                numerator /= gcd;
                denominator /= gcd;
            }
            this.denominator = denominator;
        }

        public static Fraction Parse(string s)
        {
            if(TryParse(s, out Fraction fraction))
            {
                return fraction;
            }
            else
            {
                throw new FormatException("Input string was not in a correct format.");
            }
        }

        public static bool TryParse(string s, out Fraction fraction)
        {
            fraction = null;
            Regex pattern1 = new Regex(@"^(-?\d+)/(\d+)$");
            Regex pattern2 = new Regex(@"^(-?\d+)$");
            Regex pattern3 = new Regex(@"^(-?\d+)\((\d+)/(\d+)\)$"); ;
            Regex pattern4 = new Regex(@"^(-?\d+)[,|\.](\d+)$");
            if(pattern1.IsMatch(s))
            {
                Match match = pattern1.Match(s);
                fraction = new Fraction(long.Parse(match.Groups[1].Value),
                                        long.Parse(match.Groups[2].Value));
                return true;
            }
            if(pattern2.IsMatch(s))
            {
                Match match = pattern2.Match(s);
                fraction = new Fraction(long.Parse(match.Groups[1].Value));
                return true;
            }
            if(pattern3.IsMatch(s))
            {
                Match match = pattern3.Match(s);
                long integral = long.Parse(match.Groups[1].Value);
                int sign = integral > 0 ? 1 : -1;
                long numerator = long.Parse(match.Groups[2].Value);
                long denominator = long.Parse(match.Groups[3].Value);
                fraction = new Fraction((Math.Abs(integral) * denominator + numerator) * sign,
                                        denominator);
                return true;
            }
            if(pattern4.IsMatch(s))
            {
                Match match = pattern4.Match(s);
                long integral = long.Parse(match.Groups[1].Value);
                int sign = integral > 0 ? 1 : -1;
                string floating = match.Groups[2].Value;
                long denominator = BinPow(10, floating.Length);
                fraction = new Fraction((Math.Abs(integral) * denominator + long.Parse(floating))
                                        * sign,
                                        denominator);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return ToString("F");
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if(string.IsNullOrEmpty(format))
            {
                format = "F";
            }
            if(format == "IF")
            {
                if(Math.Abs(numerator) > denominator && denominator != 1)
                {
                    long integral = numerator / denominator;
                    return $"{integral}({Math.Abs(numerator) % denominator}/{denominator})";
                }
                else
                {
                    format = "I";
                }
            }
            if(format == "I")
            {
                if(Math.Abs(numerator) > denominator)
                {
                    long integral = numerator / denominator;
                    return integral.ToString();
                }
                else
                {
                    format = "F";
                }
            }
            if(format == "F")
            {
                return $"{numerator}/{denominator}";
            }
            else if(format == "D")
            {
                return GetDoubleValue().ToString();
            }
            else if(new Regex(@"D\d*").IsMatch(format))
            {
                return GetDoubleValue().ToString("F" + format.Substring(1));
            }
            else
            {
                throw new FormatException($"The \"{format}\" format is not supported.");
            }

        }

        public int CompareTo(Fraction fraction)
        {
            long lcm = denominator * fraction.Denominator /
                       GCD(Math.Abs(denominator), Math.Abs(fraction.Denominator));

            long n1 = numerator * (lcm / denominator);
            long n2 = fraction.Numerator * (lcm / fraction.Denominator);
            if(n1 > n2)
                return 1;
            else if(n1 < n2)
                return -1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Fraction fraction &&
                   CompareTo(fraction) == 0;
        }

        public override int GetHashCode()
        {
            int hashCode = -671859081;
            hashCode = hashCode * -1521134295 + numerator.GetHashCode();
            hashCode = hashCode * -1521134295 + denominator.GetHashCode();
            return hashCode;
        }

        static long GCD(long a, long b)
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

        static long BinPow(long a, long n)
        {
            if(n == 0)
                return 1;
            if(n % 2 == 1)
                return a * BinPow(a, n - 1);

            long b = BinPow(a, n / 2);
            return b * b;
        }

        public class DecreaseComparer : IComparer<Fraction>
        {
            public int Compare(Fraction a, Fraction b)
            {
                return b.CompareTo(a);
            }
        }

        double GetDoubleValue() =>
            (double)numerator / denominator;

        public static explicit operator int(Fraction f) =>
            f.ToInt32(null);

        public static explicit operator long(Fraction f) =>
            f.ToInt64(null);

        public static explicit operator float(Fraction f) =>
            f.ToSingle(null);

        public static explicit operator double(Fraction f) =>
            f.ToDouble(null);

        public static explicit operator decimal(Fraction f) =>
            f.ToDecimal(null);

        public TypeCode GetTypeCode() =>
            TypeCode.Object;

        public bool ToBoolean(IFormatProvider provider) =>
            Numerator != 0;

        public char ToChar(IFormatProvider provider) =>
            Convert.ToChar(GetDoubleValue(), provider);

        public sbyte ToSByte(IFormatProvider provider) =>
            Convert.ToSByte(GetDoubleValue(), provider);

        public byte ToByte(IFormatProvider provider) =>
            Convert.ToByte(GetDoubleValue(), provider);

        public short ToInt16(IFormatProvider provider) =>
            Convert.ToInt16(GetDoubleValue(), provider);

        public ushort ToUInt16(IFormatProvider provider) =>
            Convert.ToUInt16(GetDoubleValue(), provider);

        public int ToInt32(IFormatProvider provider) =>
            Convert.ToInt32(GetDoubleValue(), provider);

        public uint ToUInt32(IFormatProvider provider) =>
            Convert.ToUInt32(GetDoubleValue(), provider);

        public long ToInt64(IFormatProvider provider) =>
            Convert.ToInt64(GetDoubleValue(), provider);

        public ulong ToUInt64(IFormatProvider provider) =>
            Convert.ToUInt64(GetDoubleValue(), provider);

        public float ToSingle(IFormatProvider provider) =>
            Convert.ToSingle(GetDoubleValue(), provider);

        public double ToDouble(IFormatProvider provider) =>
            GetDoubleValue();

        public decimal ToDecimal(IFormatProvider provider) =>
            Convert.ToDecimal(GetDoubleValue(), provider);

        public DateTime ToDateTime(IFormatProvider provider) =>
            Convert.ToDateTime(GetDoubleValue(), provider);

        public string ToString(IFormatProvider provider) =>
            ToString("F", provider);

        public object ToType(Type conversionType, IFormatProvider provider) =>
            Convert.ChangeType(GetDoubleValue(), conversionType);


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
            long lcm = a.Denominator * b.Denominator /
                      GCD(Math.Abs(a.Denominator), Math.Abs(b.Denominator));

            long n1 = a.Numerator * (lcm / a.Denominator);
            long n2 = b.Numerator * (lcm / b.Denominator);
            return new Fraction(n1 + n2, lcm);
        }

        public static Fraction operator -(Fraction a) =>
            a * -1;

        public static Fraction operator -(Fraction a, Fraction b) =>
            a + (-b);

        public static Fraction operator *(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static Fraction operator *(Fraction a, long b) =>
            new Fraction(a.Numerator * b, a.Denominator);

        public static Fraction operator /(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator); 
    }
}