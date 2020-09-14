using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var list = new List<IPAddress>() { IPAddress.Parse("192.168.1.65"), IPAddress.Parse("192.168.1.66") };
            var list = new string[] { "192.168.1.65", "192.168.1.66" };

            Assert.AreEqual("192.168.1.64/30", Program.CalculateMinSubnetIp(list));            
        }
        [TestMethod]
        public void TestMethod2()
        {
            var list = new string[] { "192.168.1.17", "192.168.1.30" };

            Assert.AreEqual("192.168.1.16/28", Program.CalculateMinSubnetIp(list));
        }
        [TestMethod]
        public void TestMethod3()
        {
        var list = new string[] { "192.168.1.17", "192.168.1.33" };

            Assert.AreEqual("192.168.1.0/26", Program.CalculateMinSubnetIp(list));
        }
    }
}
