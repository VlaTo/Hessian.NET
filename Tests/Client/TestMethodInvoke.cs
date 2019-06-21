using System.Diagnostics;
using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraProgramming.Serialization.Tests.Client
{
    [TestClass]
    public class TestMethodInvoke : ClientContextBase
    {
        protected HessianResponse Response
        {
            get;
            private set;
        }

        protected override CallOptions CallOptions => new CallOptions
        {

        };

        protected override async Task ActAsync()
        {
            Response = await TestClient.TestMethodAsync("test", CallOptions);
            Debug.WriteLine("[TestMethodInvoke.ActAsync] complete");
        }

        [TestMethod]
        public void MethodCalled()
        {
            Assert.IsNotNull(Response, "Error");
        }
    }
}