using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            UnregularTask.Program.Main(new string[] { });
            Assert.AreEqual("Yes", UnregularTask.Program.StartMethod(new int[] { -8,  2, 0, 5, 6}, 4));
        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual("Yes", UnregularTask.Program.StartMethod(new int[] { 1, 2, 0, 5, 6 }, 4));
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual("No", UnregularTask.Program.StartMethod(new int[] { -8, 2, 3, 5, 6 }, 4));
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual("No", UnregularTask.Program.StartMethod(new int[] { 1, 2, 3, 5, 6 }, 4));
        }
    }
}
