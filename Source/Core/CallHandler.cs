using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    internal class CallHandler
    {
        private readonly ITaskQueue taskQueue;
        private readonly Channel channel;

        public CallHandler(ITaskQueue taskQueue, Channel channel)
        {
            this.taskQueue = taskQueue ?? throw new ArgumentNullException(nameof(taskQueue));
            this.channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="payload"></param>
        /// <param name="context"></param>
        /// <param name="tcs"></param>
        /// <param name="receivedMessageCallback"></param>
        public void ClientCall<TResponse>(
            byte[] payload,
            SendCallContext context,
            TaskCompletionSource<TResponse> tcs,
            IReceivedMessageCallback receivedMessageCallback
        )
            where TResponse : class
        {
            var task = Task.Factory.StartNew(async () =>
            {
                await channel.SendAsync(payload);
                Debug.WriteLine("[CallHandler.ClientCall] send complete");
                var buffer = await channel.ReceiveAsync();
                var response = Activator.CreateInstance<TResponse>();
                receivedMessageCallback.OnClientResponse(true, buffer);
                Debug.WriteLine("[CallHandler.ClientCall] receive complete");
                tcs.SetResult(response);
            });

            taskQueue.Add(task);
        }
    }
}