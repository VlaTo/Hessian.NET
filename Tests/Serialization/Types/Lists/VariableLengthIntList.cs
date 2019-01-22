using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Lists
{
    [TestClass]
    public sealed class VariableLengthIntList : SerializerContextBase
    {
        protected override object Data { get; } = new Iterator<int>(Enumerable.Range(0, 5));

        [TestMethod]
        public void Check0()
        {
            Assert.AreEqual(21, Output.Length);
        }

        public sealed class Iterator<TValue> : IEnumerable<TValue>
        {
            private readonly IEnumerable<TValue> enumerable;

            public Iterator(IEnumerable<TValue> enumerable)
            {
                this.enumerable = enumerable;
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return enumerable.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}