using System;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    public class DefaultCallInvoker : CallInvoker
    {
        private readonly Channel channel;

        public DefaultCallInvoker(Channel channel)
        {
            if (null == channel)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            this.channel = channel;
        }

        public override AsyncClientStreamingCall<TRequest, TResponse> CreateAsyncClientStreamingCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, TRequest request, CallOptions options)
        {
            var call = CreateCallDetails(method, host, options);
            return Calls.AsyncClientStreamingCall(call);
        }

        protected virtual CallInvocationDetails<TRequest, TResponse> CreateCallDetails<TRequest, TResponse>(
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options)
            where TRequest : class
            where TResponse : class
        {
            return new CallInvocationDetails<TRequest, TResponse>(channel, method, host, options);
        }
    }
}