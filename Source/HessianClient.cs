using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class HessianClient : ClientBase<HessianClient>
    {
        public HessianClient(Channel channel)
            : base(channel)
        {
        }
        public HessianClient(Channel channel, CallInvoker invoker)
            : base(channel, invoker)
        {
        }

        public virtual Task<HessianResponse> CallAsync(HessianCall call, CallOptions options)
        {
            var method = new Method<HessianCall, HessianResponse>();
            var temp = Invoker.CreateAsyncClientStreamingCall(method, "",  options);
        }
    }
}