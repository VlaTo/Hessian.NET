using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Arrays.OneDimensional
{
    [TestClass]
    public class ShortByteArray : SerializerContextBase
    {
        protected override object Data => new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            Assert.AreEqual(11, Output.Length);
        }

        [TestMethod]
        public void Check2()
        {
            Assert.AreEqual(0x2A, Output[0]);
        }
    }
}