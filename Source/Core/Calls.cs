namespace LibraProgramming.Serialization.Hessian.Core
{
    internal class Calls
    {
        public static AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(CallInvocationDetails<TRequest, TResponse> invocation)
            where TRequest : class
            where TResponse : class
        {
            var asyncCall = new AsyncCall<TRequest, TResponse>(invocation);
            var resultTask = asyncCall.ClientStreamingCallAsync();
            var requestStream = new ClientRequestStream<TRequest, TResponse>(asyncCall);
            return new AsyncClientStreamingCall<TRequest, TResponse>(requestStream, resultTask, asyncCall.ResponseHeadersAsync, asyncCall.GetStatus, asyncCall.GetTrailers, asyncCall.Cancel);
        }
    }
}