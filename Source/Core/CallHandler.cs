using System;
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
            SendCallContext<TResponse> context,
            IReceivedMessageCallback<TResponse> receivedMessageCallback
        )
            where TResponse : class
        {
            var task = Task.Factory.StartNew(async () =>
            {
                await channel.SendAsync(payload);

                var buffer = await channel.ReceiveAsync();

                receivedMessageCallback.OnClientResponse(true, context.Completion, buffer);
            });

            taskQueue.Add(task);
        }
    }
}