using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian;

namespace LibraProgramming.Serialization.Tests.Client
{
    public abstract class ClientContextBase : TestContextBase
    {
        protected TestClient TestClient
        {
            get;
            private set;
        }

        protected abstract CallOptions CallOptions
        {
            get;
        }

        protected override Task ArrangeAsync()
        {
            var channel = new TestChannel();
            TestClient = new TestClient(channel);
            return Task.CompletedTask;
        }
    }
}