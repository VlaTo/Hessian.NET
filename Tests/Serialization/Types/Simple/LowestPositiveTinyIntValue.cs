using System;
using LibraProgramming.Serialization.Hessian;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Simple
{
    [TestClass]
    public sealed class LowestPositiveTinyIntValue : SimpleSerializationTestContext
    {
        protected override Action<HessianOutputWriter> Action => writer => writer.WriteInt32(0);

        [TestMethod]
        public void NumberSerialized()
        {
            Assert.IsNotNull(Data);
        }

        [TestMethod]
        public void ExactLength()
        {
            Assert.AreEqual(1,Data.Length);
        }

        [TestMethod]
        public void ExactValue()
        {
            Assert.AreEqual(0x90, Data[0]);
        }
    }
}