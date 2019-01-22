using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Lists
{
    [TestClass]
    public sealed class ShortIntList : SerializerContextBase
    {
        protected override object Data => new List<int>(Enumerable.Range(0, 5));

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            Assert.AreEqual(14, Output.Length);
        }

        [TestMethod]
        public void Check2()
        {
            Assert.AreEqual(0x75, Output[0]);
        }
    }
}
