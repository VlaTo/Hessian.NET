using System;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class CallInvocationDetails<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly Channel channel;
        private readonly Method<TRequest, TResponse> method;
        private readonly WorkingTaskQueue taskQueue;

        /// <summary>
        /// 
        /// </summary>
        public ITaskQueue TaskQueue => taskQueue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        protected CallInvocationDetails(
            Channel channel,
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options)
        {
            if (null == channel)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            if (null == method)
            {
                throw new ArgumentNullException(nameof(method));
            }

            this.channel = channel;
            this.method = method;
            taskQueue = new WorkingTaskQueue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract byte[] Serialize(TRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public abstract TResponse Deserialize(byte[] payload);

        internal virtual CallHandler CreateCallHandler()
        {
            return new CallHandler(taskQueue, channel);
        }
    }
}