using LibraProgramming.Serialization.Hessian.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Arrays.OneDimensional
{
    [TestClass]
    public class SingleChunkByteArray : SerializerContextBase
    {
        protected override object Data => new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF, 0x10};

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            Assert.AreEqual(20, Output.Length);
        }

        [TestMethod]
        public void Check2()
        {
            Assert.AreEqual(Marker.BinaryFinalChunk, Output[0]);
        }
    }
}