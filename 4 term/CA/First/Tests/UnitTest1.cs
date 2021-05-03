using Microsoft.VisualStudio.TestTools.UnitTesting;
using First;
using System;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd()
        {
            for(int i = sbyte.MinValue; i <= sbyte.MaxValue; i++)
            {
                for(int j = sbyte.MinValue; j <= sbyte.MaxValue; j++)
                {
                    sbyte b1 = (sbyte)i, b2 = (sbyte)j;

                    sbyte a = b1;
                    a += b2;

                    sbyte b = b1;
                    b -= b2;

                    sbyte c = b1;
                    c *= b2;

                    sbyte e = b1;
                    if(b2 != 0)
                    {
                        e /= b2;
                    }

                    Integer i1 = new Integer(i, 8, false);
                    Integer i2 = new Integer(j, 8, false);
                    Assert.AreEqual(a, (i1 + i2).ToInt());
                    Assert.AreEqual(b, (i1 - i2).ToInt());
                    Assert.AreEqual(c, (i1 * i2).ToInt());
                    if(b2 != 0)
                    {
                        Assert.AreEqual(e, (i1 / i2).ToInt());
                    }
                }           
            }
        }
    }
}
