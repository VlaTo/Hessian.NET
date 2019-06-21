using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian;

namespace LibraProgramming.Serialization.Tests.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class TestClient : HessianClientBase<TestClient>, ITestClient
    {
        private readonly Method<HessianCall, HessianResponse> method1;
        private readonly string host;

        public TestClient(Channel channel)
            : this(new DefaultCallInvoker(channel))
        {
        }

        public TestClient(CallInvoker invoker)
            : base(invoker)
        {
            host = "";
            method1 = new Method<HessianCall, HessianResponse>(MethodType.ClientStreaming, "Test");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual Task<HessianResponse> TestMethodAsync(string arg1, CallOptions options)
        {
            var call = new HessianCall { };
            return Invoker.CreateAsyncCall(method1, host, options, call).ResponseAsync;
        }
    }
}