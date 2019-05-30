using LibraProgramming.Serialization.Hessian.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Arrays.OneDimensional
{
    [TestClass]
    public class MultiChunksByteArray : SerializerContextBase
    {
        protected override object Data { get; } = new byte[32769];

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            // 'b' b1 b0 <binary-data> 'B' b1 b0 <binary-data>
            Assert.AreEqual(1 + 2 + 32769 + 1 + 2, Output.Length);
        }

        [TestMethod]
        public void Check2()
        {
            // 'b' b1 b0 <binary-data>
            Assert.AreEqual(Marker.BinaryNonFinalChunk, Output[0]);
            Assert.AreEqual(0x80, Output[1]);
            Assert.AreEqual(0x00, Output[2]);

            // 'B' b1 b0 <binary-data>
            Assert.AreEqual(Marker.BinaryFinalChunk, Output[32771]);
            Assert.AreEqual(0x00, Output[32772]);
            Assert.AreEqual(0x01, Output[32773]);
        }
    }
}