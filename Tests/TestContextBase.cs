using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests
{
    public abstract class TestContextBase
    {
        [TestInitialize]
        public void Setup()
        {
            Arrange();
            Act();
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
        }

        protected abstract void Arrange();

        protected abstract void Act();
    }
}