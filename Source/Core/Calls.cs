namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Calls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="callInvocation"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static AsyncClientCall<TResponse> AsyncClientCall<TRequest, TResponse>(
            CallInvocationDetails<TRequest, TResponse> callInvocation,
            TRequest request
        )
            where TRequest : class
            where TResponse : class
        {
            var asyncCall = new AsyncCall<TRequest, TResponse>(callInvocation);
            var resultTask = asyncCall.ClientCallAsync(request);

            return new AsyncClientCall<TResponse>(resultTask, asyncCall.Cancel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="callInvocation"></param>
        /// <returns></returns>
        public static AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(
            CallInvocationDetails<TRequest, TResponse> callInvocation
        )
            where TRequest : class
            where TResponse : class
        {
            var asyncCall = new AsyncCall<TRequest, TResponse>(callInvocation);
            var resultTask = asyncCall.ClientStreamingCallAsync();
            var requestStream = new ClientRequestStream<TRequest, TResponse>(asyncCall);

            return new AsyncClientStreamingCall<TRequest, TResponse>(requestStream, resultTask, asyncCall.Cancel);
        }
    }
}