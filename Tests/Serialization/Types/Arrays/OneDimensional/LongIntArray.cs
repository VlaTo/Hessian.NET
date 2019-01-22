using LibraProgramming.Serialization.Hessian.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Arrays.OneDimensional
{
    [TestClass]
    public class LongIntArray : SerializerContextBase
    {
        protected override object Data => new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            Assert.AreEqual(21, Output.Length);
        }

        [TestMethod]
        public void Check2()
        {
            Assert.AreEqual(Marker.FixedLengthList, Output[0]);
        }
    }
}