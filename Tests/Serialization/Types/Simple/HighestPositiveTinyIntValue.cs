using System;
using LibraProgramming.Serialization.Hessian;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Simple
{
    [TestClass]
    public sealed class HighestPositiveTinyIntValue : SimpleSerializationTestContext
    {
        protected override Action<HessianOutputWriter> Action => writer => writer.WriteInt32(16);

        [TestMethod]
        public void NumberSerialized()
        {
            Assert.IsNotNull(Data);
        }

        [TestMethod]
        public void ExactLength()
        {
            Assert.AreEqual(1, Data.Length);
        }

        [TestMethod]
        public void ExactValue()
        {
            Assert.AreEqual(0xA0, Data[0]);
        }
    }
}