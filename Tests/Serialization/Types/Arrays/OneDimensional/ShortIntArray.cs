using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Arrays.OneDimensional
{
    [TestClass]
    public  class ShortIntArray : SerializerContextBase
    {
        protected override object Data => new[] {1, 2, 3};

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            Assert.AreEqual(17, Output.Length);
        }

        [TestMethod]
        public void Check2()
        {
            Assert.AreEqual(0x73, Output[0]);
        }
    }
}
