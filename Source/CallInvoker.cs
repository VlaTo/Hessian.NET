using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian
{
    public abstract class CallInvoker
    {
        public abstract AsyncClientStreamingCall<TResponse> CreateAsyncClientStreamingCall<TRequest, TResponse>(
            Method<TRequest, TResponse> method,
            string host,
            TRequest request,
            CallOptions options)
            where TRequest : class
            where TResponse : class;
    }
}