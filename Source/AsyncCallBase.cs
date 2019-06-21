using System;
using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    internal abstract class AsyncCallBase<TWrite, TRead> : IReceivedMessageCallback, ISendCompletionCallback
    {
        private readonly object syncRoot;

        protected Func<TWrite, byte[]> Serialize
        {
            get;
        }

        protected Func<byte[], TRead> Deserialize
        {
            get;
        }

        protected object SyncRoot => syncRoot;

        protected AsyncCallBase(Func<TWrite, byte[]> serialize, Func<byte[], TRead> deserialize)
        {
            if (null == serialize)
            {
                throw new ArgumentNullException(nameof(serialize));
            }

            if (null == deserialize)
            {
                throw new ArgumentNullException(nameof(deserialize));
            }

            Serialize = serialize;
            Deserialize = deserialize;

            syncRoot = new object();
        }

        public void Cancel()
        {
            lock (SyncRoot)
            {
                //call.Cencel();
            }
        }

        protected Task SendMessageInternalAsync(TWrite message)
        {
            var payload = Serialize.Invoke(message);

            lock (SyncRoot)
            {
                var tcs = new TaskCompletionSource();
                //call.StartSendMessage(SendCompletionCallback, payload, writeFlags, !initialMetadataSent);

                return tcs.Task;
            }
        }

        void IReceivedMessageCallback.OnClientResponse(bool success, byte[] payload)
        {
        }
    }
}