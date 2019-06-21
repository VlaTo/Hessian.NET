namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CallInvoker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract AsyncClientCall<TResponse> CreateAsyncCall<TRequest, TResponse>(
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options,
            TRequest request)
            where TRequest : class
            where TResponse : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract AsyncClientStreamingCall<TRequest, TResponse> CreateAsyncClientStreamingCall<TRequest, TResponse>(
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options)
            where TRequest : class
            where TResponse : class;
    }
}