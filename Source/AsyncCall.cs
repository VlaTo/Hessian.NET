using System;
using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    internal class AsyncCall<TRequest, TResponse> : AsyncCallBase<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly CallInvocationDetails<TRequest, TResponse> callInvocation;
        private CallHandler callHandler;

        internal IReceivedMessageCallback ReceivedMessageCallback => this;

        public AsyncCall(CallInvocationDetails<TRequest, TResponse> callInvocation)
            : base(callInvocation.Serialize, callInvocation.Deserialize)
        {
            if (null == callInvocation)
            {
                throw new ArgumentNullException(nameof(callInvocation));
            }

            this.callInvocation = callInvocation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<TResponse> ClientCallAsync(TRequest request)
        {
            lock (SyncRoot)
            {
                var tcs = new TaskCompletionSource<TResponse>();

                callHandler = CreateCallHandler();

                using (var context = SendCallContext.Create())
                {
                    var payload = Serialize.Invoke(request);
                    callHandler.ClientCall(payload, context, tcs, ReceivedMessageCallback);
                }

                return tcs.Task;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<TResponse> ClientStreamingCallAsync()
        {
            lock (SyncRoot)
            {
                //GrpcPreconditions.CheckState(!started);
                //started = true;

                //Initialize(details.Channel.CompletionQueue);

                //readingDone = true;

                var tcs = new TaskCompletionSource<TResponse>();

                using (var context = SendCallContext.Create())
                {
                    //call.StartClientStreaming(UnaryResponseClientCallback, metadataArray, details.Options.Flags);
                }

                return tcs.Task;
            }
        }

        public Task SendMessageAsync(TRequest request)
        {
            return SendMessageInternalAsync(request);
        }

        public Task SendCloseFromClientAsync()
        {
            return TaskEx.CompletedTask;
        }

        private CallHandler CreateCallHandler()
        {
            return callInvocation.CreateCallHandler();
        }
    }
}