using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    public class Integer
    {
        byte[] bits;
        int bitsCount;
        bool interactive;
        public Integer(int n, int bitsCount, bool interactive)
        {
            this.interactive = interactive;
            this.bitsCount = bitsCount;
            bits = ToBinary(Math.Abs(n));
            if(n < 0)
            {
                bits = ToTwosComplement(bits);
            }
        }
        public Integer(byte[] bits, bool interactive)
        {
            this.interactive = interactive;
            bitsCount = bits.Length;
            this.bits = bits;
        }

        byte[] ToBinary(int n)
        {
            byte[] bits = new byte[bitsCount];
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

        static byte[] DoubleSize(byte[] bits)
        {
            byte[] res = new byte[bits.Length * 2];
            Array.Copy(bits, res, bits.Length);
            for(int i = bits.Length; i < res.Length; i++)
            {
                res[i] = bits.Last();
            }
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

        static byte[] MultiplyBits(byte[] bits1, byte[] bits2, bool interactive)
        {
            bool overflow = false;
            byte[] res = new byte[bits1.Length];
            for(int i = 0; i < res.Length; i++)
            {
                for(int j = 0; j < res.Length; j++)
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
                Console.Write($"{i + 1}) {res[i]} + {t} = {(res[i] + t) % 2}, ");
                res[i] += t;
                t = (byte)(res[i] / 2);
                Console.WriteLine($"t = {t}");
                res[i] %= 2;
            }
            if(interactive && (overflow || t != 0))
            {
                Console.WriteLine("Overflow!");
            }
            return res;
        }

        public int ToInt()
        {
            byte[] bits = (byte[])this.bits.Clone();
            bool isNeg = false;
            if(bits.Last() == 1)
            {
                bits = ToTwosComplement(bits);
                isNeg = true;
            }
            int res = 0;
            for(int i = 0; i < bitsCount; i++)
            {
                res += (1 << i) * bits[i];
            }
            if(isNeg)
            {
                res *= -1;
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

        static byte[] DivideBits(byte[] divident, byte[] divider, bool interactive)
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

        public override string ToString()
        {
            return ToInt().ToString();
        }
        public static Integer operator >>(Integer a, int shifts)
        {
            return new Integer(ShiftRight(a.bits, shifts), a.interactive);
        }

        public static Integer operator <<(Integer a, int shifts)
        {
            return new Integer(ShiftLeft(a.bits, shifts), a.interactive);
        }

        public static Integer operator +(Integer a, Integer b)
        {
            if(a.interactive)
            {
                Console.WriteLine($"Number {a.ToInt()} in two's complement: {BitsToString(a.bits)}");
                Console.WriteLine($"Number {b.ToInt()} in two's complement: {BitsToString(b.bits)}");
            }
            byte aSign = a.bits.Last(), bSign = b.bits.Last();
            Integer res = new Integer(AddBits(a.bits, b.bits, a.interactive), a.interactive);
            byte resSign = res.bits.Last();
            if(a.interactive)
            {
                if(aSign == bSign && aSign != resSign)
                {
                    Console.WriteLine("Overflow!");
                }
                Console.WriteLine($"Result: {res.ToInt()}({BitsToString(res.bits)})");
            }
            
            return res;
        }

        public static Integer operator -(Integer a, Integer b)
        {
            return a + (-b);  
        }

        public static Integer operator -(Integer a)
        {
            return new Integer(ToTwosComplement(a.bits), a.interactive);
        }

        public static Integer operator *(Integer a, Integer b)
        {
            if(a.interactive)
            {
                Console.WriteLine($"Number {a.ToInt()} in two's complement: {BitsToString(a.bits)}");
                Console.WriteLine($"Number {b.ToInt()} in two's complement: {BitsToString(b.bits)}");
            }
            Integer res = new Integer(MultiplyBits(a.bits, b.bits, a.interactive), a.interactive);
            if(a.interactive)
            {
                Console.WriteLine($"Result: {res.ToInt()}({BitsToString(res.bits)})");
            }
            return res;
        }

        public static Integer operator /(Integer a, Integer b)
        {
            if(a.interactive)
            {
                Console.WriteLine($"Number {a.ToInt()} in two's complement: {BitsToString(a.bits)}");
                Console.WriteLine($"Number {b.ToInt()} in two's complement: {BitsToString(b.bits)}");
            }
            Integer res = new Integer(DivideBits(a.bits, b.bits, a.interactive), a.interactive);
            if(a.interactive)
            {
                Console.WriteLine($"Result: {res.ToInt()}({BitsToString(res.bits)})");
            }
            return res;
        }
        
    }
}
