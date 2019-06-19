namespace LibraProgramming.Serialization.Hessian
{
    public class CallInvocationDetails<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public CallInvocationDetails(Channel channel, Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}