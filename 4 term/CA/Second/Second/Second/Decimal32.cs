using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    class Decimal32
    {
        byte sign;
        byte[] exponent;
        byte[] mantissa;
        public bool Interactive { get; set; }
        public Decimal32(string number, bool interactive) : this(float.Parse(number), interactive)
        {
        }


        public Decimal32(float number, bool interactive)
        {
            exponent = new byte[8];
            mantissa = new byte[23];
            Interactive = interactive;
            if(number < 0)
            {
                sign = 1;
                number = Math.Abs(number);
            }
            if(number == 0)
            {
                return;
            }

            int intPart = (int)number;
            List<byte> m = ToBinaryList(intPart);
            int len = m.Count;
            int exp = 0;
            List<byte> m1 = ToBinaryList(number % 1, 23 - len + 1);
            m1.AddRange(m);
            m = m1;

            if(intPart == 0)
            {
                while(m.Count > 0 && m.Last() == 0)
                {
                    exp--;
                    m.RemoveAt(m.Count - 1);
                }
            }
            else
            {
                exp = len - 1;
            }
            exp += 127;
            exponent = ToBinary(exp, 8);
            for(int i = m.Count - 2; i >= 0; i--)
            {
                mantissa[22 - (m.Count - i - 2)] = m[i];
            }
        }

        public Decimal32(byte sign, byte[] exponent, byte[] mantissa, bool interactive)
        {
            this.sign = sign;
            this.exponent = exponent;
            this.mantissa = mantissa;
            Interactive = interactive;
        }

        public float ToFloat()
        {
            float ans = 0;
            for(int i = 22; i >= 0; i--)
            {
                ans += mantissa[i] * (1f / (1 <<  (23 - i)));
            }
            ans += 1;
            sbyte exp = (sbyte)BitsToInt(exponent);
            if(exp == 0)
            {
                return 0;
            }
            exp -= 127;
            if(exp > 0)
            {
                ans *= (1 << exp);
            }
            else
            {
                ans *= 1f / (1 << Math.Abs(exp));
            }
            ans *= sign == 1 ? -1 : 1;
            return ans;
        }

        public static Decimal32 operator-(Decimal32 a)
        {
            byte sign = a.sign == 1 ? (byte)0 : (byte)1;
            byte[] exponent = new byte[8];
            Array.Copy(a.exponent, exponent, 8);
            byte[] mantissa = new byte[23];
            Array.Copy(a.mantissa, mantissa, 23);
            return new Decimal32(sign, exponent, mantissa, a.Interactive);
        }

        public static Decimal32 operator *(Decimal32 a, Decimal32 b)
        {
            int expA = BitsToInt(a.exponent) - 127, expB = BitsToInt(b.exponent) - 127;
            int exp = expA + expB;
            byte sign = (byte)((a.sign == 1 ^ b.sign == 1) ? 1 : 0);

            if(a.Interactive)
            {
                Console.WriteLine($"{a.ToFloat()} * {b.ToFloat()}:");
            }

            byte[] mantissaA = new byte[24];
            Array.Copy(a.mantissa, 0, mantissaA, 0, 23);
            mantissaA[23] = 1;

            byte[] mantissaB = new byte[24];
            Array.Copy(b.mantissa, 0, mantissaB, 0, 23);
            mantissaB[23] = 1;
            if(a.Interactive)
            {
                Console.WriteLine($"{a.sign} {BitsToString(a.exponent)} {BitsToString(a.mantissa)}");
                Console.WriteLine($"{b.sign} {BitsToString(b.exponent)} {BitsToString(b.mantissa)}");
                Console.WriteLine("Multiplying bits:");
            }

            byte[] newMantissa = MultiplyBits(mantissaA, mantissaB, a.Interactive);

            if(a.Interactive)
            {
                Console.WriteLine($"The resulting mantissa is {BitsToString(newMantissa)}");
            }
            int shift = 0;
            for(int i = 47; i >= 0; i--)
            {
                shift++;
                if(newMantissa[i] == 1)
                {
                    break;
                }
   
            }

            newMantissa = ShiftLeft(newMantissa, shift);
            if(a.Interactive)
            {
                Console.WriteLine($"Normalized mantissa is {BitsToString(newMantissa)}");
            }
            byte[] newMantissa1 = new byte[23];
            Array.Copy(newMantissa, 25, newMantissa1, 0, 23);

            if(a.Interactive)
            {
                Console.WriteLine($"Result:");
                Console.WriteLine($"{sign} {BitsToString(ToBinary(exp + 127 + 2 - shift, 8))} {BitsToString(newMantissa1)}");
            }
            return new Decimal32(sign, ToBinary(exp + 127 + 2 - shift, 8), newMantissa1, a.Interactive);
        }

        public static Decimal32 operator /(Decimal32 a, Decimal32 b)
        {
            int expA = BitsToInt(a.exponent) - 127, expB = BitsToInt(b.exponent) - 127;
            int exp = expA - expB;
            byte sign = (byte)((a.sign == 1 ^ b.sign == 1) ? 1 : 0);
            if(a.Interactive)
            {
                Console.WriteLine($"{a.ToFloat()} / {b.ToFloat()}:");
            }

            byte[] mantissaA = new byte[49];
            Array.Copy(a.mantissa, 0, mantissaA, 24, 23);
            mantissaA[47] = 1;

            byte[] mantissaB = new byte[49];
            Array.Copy(b.mantissa, 0, mantissaB, 0, 23);
            mantissaB[23] = 1;
            if(a.Interactive)
            {
                Console.WriteLine($"{a.sign} {BitsToString(a.exponent)} {BitsToString(a.mantissa)}");
                Console.WriteLine($"{b.sign} {BitsToString(b.exponent)} {BitsToString(b.mantissa)}");
                Console.WriteLine("Increasing size of mantissas:");
                Console.WriteLine($"{BitsToString(mantissaA)}");
                Console.WriteLine($"{BitsToString(mantissaB)}");
                Console.WriteLine("Dividing bits:");
            }

            byte[] newMantissa = DivideBits(mantissaA, mantissaB);
            if(a.Interactive)
            {
                Console.WriteLine($"The resulting mantissa is {BitsToString(newMantissa)}");
            }

            int shift = 0;
            for(int i = newMantissa.Length - 1; i >= 0; i--)
            {
                shift++;
                if(newMantissa[i] == 1)
                {
                    break;
                }

            }

            newMantissa = ShiftLeft(newMantissa, shift - 1);
            if(a.Interactive)
            {
                Console.WriteLine($"Normalized mantissa is {BitsToString(newMantissa)}");
            }
            byte[] newMantissa1 = new byte[23];
            Array.Copy(newMantissa, 25, newMantissa1, 0, 23);

            if(a.Interactive)
            {
                Console.WriteLine($"Result:");
                Console.WriteLine($"{sign} {BitsToString(ToBinary(exp + 127 + 25 - shift, 8))} {BitsToString(newMantissa1)}");
            }
            return new Decimal32(sign, ToBinary(exp + 127 + 25 - shift, 8), newMantissa1, a.Interactive);
        }

        public static Decimal32 operator +(Decimal32 a, Decimal32 b)
        {
            if(a.sign != b.sign)
            {
                if(a.sign == 0 && b.sign == 1)
                {
                    return a - (-b);
                }
                else
                {
                    return b - (-a);
                }
            }
            if(a.Interactive)
            {
                Console.WriteLine($"{a.ToFloat()} + {b.ToFloat()}:");
            }
            int expA = BitsToInt(a.exponent), expB = BitsToInt(b.exponent);
            if(expA < expB)
            {
                (a, b) = (b, a);
                (expA, expB) = (expB, expA);
                if(a.Interactive)
                {
                    Console.WriteLine($"Exponent of A is less than exponent of B, swapping numbers");
                }
            }
            int delta = expA - expB;

            if(a.Interactive)
            {
                Console.WriteLine($"{a.sign} {BitsToString(a.exponent)} {BitsToString(a.mantissa)}");
                Console.WriteLine($"{b.sign} {BitsToString(b.exponent)} {BitsToString(b.mantissa)}");
            }

            byte[] mantissaA = new byte[25];
            Array.Copy(a.mantissa, 0, mantissaA, 0, 23);
            mantissaA[23] = 1;

            byte[] mantissaB = new byte[25];
            Array.Copy(b.mantissa, 0, mantissaB, 0, 23);
            mantissaB[23] = 1;

            if(a.Interactive)
            {
                Console.WriteLine($"Exponents differs by {delta}, shifting b mantissa right by {delta} bits");

            }
            mantissaB = ShiftRight(mantissaB, delta);
            if(a.Interactive)
            {
                Console.WriteLine($"{a.sign} {BitsToString(a.exponent)} {BitsToString(mantissaA)}");
                Console.WriteLine($"{b.sign} {BitsToString(b.exponent)} {BitsToString(mantissaB)}");
                Console.WriteLine("Adding bits:");
            }

            byte[] newMantissa = AddBits(mantissaA, mantissaB, a.Interactive);
            if(newMantissa.Last() == 1)
            {
                expA++;
                newMantissa = ShiftRight(newMantissa, 1);
            }
            if(a.Interactive)
            {
                Console.WriteLine($"The resulting mantissa is {BitsToString(newMantissa)}");
            }
            byte[] newMantissa1 = new byte[23];
            Array.Copy(newMantissa, 0, newMantissa1, 0, 23);
            newMantissa = newMantissa1;
            if(a.Interactive)
            {
                Console.WriteLine($"Normalized mantissa is {BitsToString(newMantissa)}");
            }
            if(a.Interactive)
            {
                Console.WriteLine($"Result:");
                Console.WriteLine($"{a.sign} {BitsToString(ToBinary(expA, 8))} {BitsToString(newMantissa)}");
            }
            return new Decimal32(a.sign, ToBinary(expA, 8), newMantissa, a.Interactive);
        }
        public static Decimal32 operator -(Decimal32 a, Decimal32 b)
        {
            if(a.sign == 0 && b.sign == 1)
            {
                return a + -b;
            }
            if(a.sign == 1 && b.sign == 0)
            {
                return a + -b;
            }
            if(a.sign == 1 && b.sign == 1)
            {
                return -b - -a;
            }

            byte sign = 0;
            if(a.Interactive)
            {
                Console.WriteLine($"{a.ToFloat()} - {b.ToFloat()}:");
            }
            int expA = BitsToInt(a.exponent), expB = BitsToInt(b.exponent);
            if(expA < expB)
            {
                (a, b) = (b, a);
                (expA, expB) = (expB, expA);
                sign = 1;
                if(a.Interactive)
                {
                    Console.WriteLine($"Exponent of A is less than exponent of B, swapping numbers");
                }
            }

            byte[] mantissaA = new byte[24];
            Array.Copy(a.mantissa, 0, mantissaA, 0, 23);
            mantissaA[23] = 1;

            byte[] mantissaB = new byte[24];
            Array.Copy(b.mantissa, 0, mantissaB, 0, 23);
            mantissaB[23] = 1;
            if(expA == expB)
            {
                if(BitsToInt(mantissaB) > BitsToInt(mantissaA))
                {
                    (mantissaA, mantissaB) = (mantissaB, mantissaA);
                    sign = 1;
                    if(a.Interactive)
                    {
                        Console.WriteLine($"Mantissa of A is less than mantissa of B, swapping mantissas, resulting sign will be -");
                    }
                }
            }
            int delta = expA - expB;

            if(a.Interactive)
            {
                Console.WriteLine($"{a.sign} {BitsToString(a.exponent)} {BitsToString(mantissaA)}");
                Console.WriteLine($"{b.sign} {BitsToString(b.exponent)} {BitsToString(mantissaB)}");
            }

            if(a.Interactive)
            {
                Console.WriteLine($"Exponents differs by {delta}, shifting b mantissa right by {delta} bits");
            }

            mantissaB = ShiftRight(mantissaB, delta);
            if(a.Interactive)
            {
                Console.WriteLine($"{a.sign} {BitsToString(a.exponent)} {BitsToString(mantissaA)}");
                Console.WriteLine($"{b.sign} {BitsToString(b.exponent)} {BitsToString(mantissaB)}");
                Console.WriteLine("Substracting bits:");
            }
            byte[] newMantissa = SubBits(mantissaA, mantissaB, a.Interactive);

            if(BitsToInt(newMantissa) == 0)
            {
                return new Decimal32(0, a.Interactive);
            }
            if(a.Interactive)
            {
                Console.WriteLine($"The resulting mantissa is {BitsToString(newMantissa)}");
            }
            int shift = 0;
            for(int i = 23; i >= 0; i--)
            {
                if(newMantissa[i] == 1)
                {
                    break;
                }
                shift++;
            }
            newMantissa = ShiftLeft(newMantissa, shift);

            if(a.Interactive)
            {
                Console.WriteLine($"Normalized mantissa is {BitsToString(newMantissa)}");
            }

            byte[] newMantissa1 = new byte[23];
            Array.Copy(newMantissa, 0, newMantissa1, 0, 23);
            if(a.Interactive)
            {
                Console.WriteLine($"Result:");
                Console.WriteLine($"{sign} {BitsToString(ToBinary(expA - shift, 8))} {BitsToString(newMantissa1)}");
            }
            return new Decimal32(sign, ToBinary(expA - shift, 8), newMantissa1, a.Interactive);
        }  

        static byte[] ToTwosComplement(byte[] bits)
        {
            byte[] res = (byte[])bits.Clone();
            for(int i = 0; i < bits.Length; i++)
            {
                res[i] = (byte)(res[i] == 0 ? 1 : 0);
            }
            byte[] one = new byte[bits.Length];
            one[0] = 1;
            return AddBits(res, one);
        }

        static byte[] DoubleSize(byte[] bits)
        {
            byte[] res = new byte[bits.Length * 2];
            Array.Copy(bits, res, bits.Length);
            for(int i = bits.Length; i < res.Length; i++)
            {
                res[i] = 0;
            }
            return res;
        }

        static byte[] DivideBits(byte[] divident, byte[] divider, bool interactive = false)
        {
            if(divider.All(x => x == 0))
            {
                throw new DivideByZeroException();
            }
            byte dividentSign = divident.Last(), dividerSign = divider.Last();
            if(dividentSign == 1)
            {
                divident = ToTwosComplement(divident);
                if(interactive)
                {
                    Console.WriteLine($"Changing sign of divident: {BitsToString(divident)}");
                }
            }
            if(dividerSign == 1)
            {
                divider = ToTwosComplement(divider);
                if(interactive)
                {
                    Console.WriteLine($"Changing sign of divider: {BitsToString(divider)}");
                }
            }

            divident = DoubleSize(divident);
            if(interactive)
            {
                Console.WriteLine($"Increasing size of divident: {BitsToString(divident)}");
            }
            byte[] M = (byte[])divider.Clone();
            byte[] A = new byte[divident.Length / 2], Q = new byte[divident.Length / 2];
            for(int i = 0; i < Q.Length; i++)
            {
                divident = ShiftLeft(divident, 1);
                Array.Copy(divident, 0, Q, 0, Q.Length);
                Array.Copy(divident, A.Length, A, 0, A.Length);
                byte ASign = A.Last();
                byte[] ABackup = A;
                if(interactive)
                {
                    Console.WriteLine($"{i + 1}) Shifting left: ");
                    Console.WriteLine($"   A = {BitsToString(A)}, Q = {BitsToString(Q)}");
                }

                if(ASign == M.Last())
                {
                    A = AddBits(A, ToTwosComplement(M));
                    if(interactive)
                    {
                        Console.WriteLine($"   Sign of A is not equal to sign of M, substracting M from A: ");
                        Console.WriteLine($"   A = {BitsToString(A)}, Q = {BitsToString(Q)}");
                    }
                }
                else
                {
                    A = AddBits(A, M);
                    if(interactive)
                    {
                        Console.WriteLine($"   Sign of A is equal to sign of M, adding M to A: ");
                        Console.WriteLine($"   A = {BitsToString(A)}, Q = {BitsToString(Q)}");
                    }
                }
                if(A.Last() == ASign || (A.All(x => x == 0) && Q.All(x => x == 0)))
                {
                    Q[0] = 1;
                    if(interactive)
                    {
                        Console.WriteLine($"   Sign of A hasn't changed, setting Q0 to 1");
                        Console.WriteLine($"   A = {BitsToString(A)}, Q = {BitsToString(Q)}");
                    }
                }
                else
                {
                    Q[0] = 0;
                    A = ABackup;
                    if(interactive)
                    {
                        Console.WriteLine($"   Sign of A has changed, setting Q0 to 0 and changing back A");
                        Console.WriteLine($"   A = {BitsToString(A)}, Q = {BitsToString(Q)}");
                    }
                }
                Array.Copy(Q, divident, Q.Length);
                Array.Copy(A, 0, divident, A.Length, A.Length);
            }
            if((dividerSign ^ dividentSign) == 1)
            {
                if(interactive)
                {
                    Console.WriteLine($"Changing sign of result: {BitsToString(Q)}");
                }
                Q = ToTwosComplement(Q);
            }
            return Q;
        }

        static byte[] MultiplyBits(byte[] bits1, byte[] bits2, bool interactive = false)
        {
            bool overflow = false;
            byte[] res = new byte[bits1.Length * 2];
            for(int i = 0; i < bits1.Length; i++)
            {
                for(int j = 0; j < bits1.Length; j++)
                {
                    if(i + j < res.Length)
                    {
                        res[i + j] += (byte)(bits1[i] * bits2[j]);
                    }
                    else if(interactive)
                    {
                        if(bits1[i] * bits2[j] > 0)
                        {
                            overflow = true;
                        }
                    }
                }
                if(interactive)
                {
                    Console.WriteLine($"{i + 1}) {BitsToString(res)}");
                }
            }
            byte t = 0;
            for(int i = 0; i < res.Length; i++)
            {
                if(interactive)
                {
                    Console.Write($"{i + 1}) {res[i]} + {t} = {(res[i] + t) % 2}, ");
                }  
                res[i] += t;
                t = (byte)(res[i] / 2);
                if(interactive)
                {
                    Console.WriteLine($"t = {t}");
                }
                
                res[i] %= 2;
            }
            if(interactive && (overflow || t != 0))
            {
                Console.WriteLine("Overflow!");
            }
            return res;
        }

        static string BitsToString(byte[] bits)
        {
            string s = "";
            for(int i = bits.Length - 1; i >= 0; i--)
            {
                s += (char)(bits[i] + '0');
            }
            return s;
        }

        static byte[] SubBits(byte[] bits1, byte[] bits2, bool interactive = false)
        {
            byte[] res = new byte[bits1.Length];
            sbyte t = 0;
            for(int i = 0; i < bits1.Length; i++)
            {
                sbyte bit = (sbyte)((sbyte)bits1[i] - (sbyte)bits2[i] + t);
                if(interactive)
                {
                    Console.Write($"{i + 1}) {bits1[i]} - {bits2[i]} - {Math.Abs(t)} = {res[i]}, ");
                }
                if(bit < 0)
                {
                    t = -1;
                    bit += 2;
                }
                else
                {
                    t = 0;
                }
                if(interactive)
                {
                    Console.WriteLine($"t = {t}");
                }
                res[i] = (byte)bit;
            }
            return res;
        }

        static byte[] AddBits(byte[] bits1, byte[] bits2, bool interactive = false)
        {
            byte[] res = new byte[bits1.Length];
            byte t = 0;

            for(int i = 0; i < bits1.Length; i++)
            {
                byte bit = (byte)(bits1[i] + bits2[i] + t);
                res[i] = (byte)(bit % 2);
                if(interactive)
                {
                    Console.Write($"{i + 1}) {bits1[i]} + {bits2[i]} + {t} = {res[i]}, ");
                }
                t = (byte)(bit > 1 ? 1 : 0);
                if(interactive)
                {
                    Console.WriteLine($"t = {t}");
                }
            }
            return res;
        }

        static byte[] ShiftRight(byte[] bits, int shifts)
        {
            byte[] res = new byte[bits.Length];
            if(shifts >= res.Length || shifts < 0)
            {
                return res;
            }
            Array.Copy(bits, shifts, res, 0, res.Length - shifts);
            return res;
        }

        static byte[] ShiftLeft(byte[] bits, int shifts)
        {
            byte[] res = new byte[bits.Length];
            if(shifts >= res.Length || shifts < 0)
            {
                return res;
            }
            Array.Copy(bits, 0, res, shifts, res.Length - shifts);
            return res;
        }

        static int BitsToInt(byte[] bits)
        {
            int res = 0;
            for(int i = 0; i < bits.Length; i++)
            {
                res += (1 << i) * bits[i];
            }
            return res;
        }

        static List<byte> ToBinaryList(int number)
        {
            List<byte> ans = new List<byte>();
            if(number == 0)
            {
                ans.Add(0);
                return ans;
            }
            while(number != 0)
            {
                ans.Add((byte)(number % 2));
                number /= 2;
            }
            return ans;
        }

        static List<byte> ToBinaryList(float number, int size)
        {
            List<byte> ans = new List<byte>();
            if(number == 0)
            {
                ans.Add(0);
                return ans;
            }
            int i = 0;
            while(i++ < size)
            {
                number *= 2;
                if(number > 1)
                {
                    ans.Add(1);
                    number -= 1;
                }
                else if(number == 1)
                {
                    ans.Add(1);
                    break;
                }
                else
                {
                    ans.Add(0);
                }
            }
            ans.Reverse();
            return ans;
        }

        static byte[] ToBinary(int n, int length)
        {
            byte[] bits = new byte[length];
            Queue<byte> subBits = new Queue<byte>();
            while(n > 0)
            {
                subBits.Enqueue((byte)(n % 2));
                n /= 2;
            }
            subBits.Enqueue(0);
            int len = 0;
            while(subBits.Count > 0 && len < bits.Length)
            {
                bits[len++] = subBits.Dequeue();
            }
            return bits;
        }
    }
}
