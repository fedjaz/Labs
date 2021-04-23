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
        bool interactive;
        public Decimal32(string number) : this(float.Parse(number))
        {
        }

        public Decimal32(float number)
        {
            exponent = new byte[8];
            mantissa = new byte[23];
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

        public float ToFloat()
        {
            float ans = 0;
            for(int i = 22; i >= 0; i--)
            {
                ans += mantissa[i] * (1f / (1 <<  (23 - i)));
            }
            ans += 1;
            SByte exp = (SByte)BitsToInt(exponent);
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

        int BitsToInt(byte[] bits)
        {
            int res = 0;
            for(int i = 0; i < bits.Length; i++)
            {
                res += (1 << i) * bits[i];
            }
            return res;
        }

        List<byte> ToBinaryList(int number)
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

        List<byte> ToBinaryList(float number, int size)
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

        byte[] ToBinary(int n, int length)
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
