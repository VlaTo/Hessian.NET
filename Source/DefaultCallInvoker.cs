using System;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultCallInvoker : CallInvoker
    {
        private readonly Channel channel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        public DefaultCallInvoker(Channel channel)
        {
            if (null == channel)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            this.channel = channel;
        }

        /// <inheritdoc cref="CallInvoker.CreateAsyncCall{TRequest,TResponse}" />
        public override AsyncClientCall<TResponse> CreateAsyncCall<TRequest, TResponse>(
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options,
            TRequest request)
        {
            var call = CreateCallDetails(method, host, options);
            return Calls.AsyncClientCall(call, request);
        }

        /// <inheritdoc cref="CallInvoker.CreateAsyncClientStreamingCall{TRequest,TResponse}" />
        public override AsyncClientStreamingCall<TRequest, TResponse> CreateAsyncClientStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            var call = CreateCallDetails(method, host, options);
            return Calls.AsyncClientStreamingCall(call);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected virtual CallInvocationDetails<TRequest, TResponse> CreateCallDetails<TRequest, TResponse>(
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options)
            where TRequest : class
            where TResponse : class
        {
            return new HessianCallInvocationDetails<TRequest, TResponse>(channel, method, host, options);
        }
    }
}