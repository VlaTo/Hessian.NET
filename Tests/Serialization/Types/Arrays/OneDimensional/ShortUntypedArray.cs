using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Arrays.OneDimensional
{
    [TestClass, Ignore]
    public class ShortUntypedArray : SerializerContextBase
    {
        //protected override object Data => new object[] {0, 1, 'c', "string"};
        protected override object Data => new ArrayList(new object[] {0, 1, 'c', "string"});

        [TestMethod]
        public void Check0()
        {
            Assert.IsNotNull(Output);
        }

        [TestMethod]
        public void Check1()
        {
            Assert.AreEqual(16, Output.Length);
        }
    }
}