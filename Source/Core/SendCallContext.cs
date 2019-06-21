using System;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal class SendCallContext<TResponse> : IDisposable
    {
        private bool disposed;

        public TaskCompletionSource<TResponse> Completion
        {
            get;
        }

        private SendCallContext(TaskCompletionSource<TResponse> tcs)
        {
            Completion = tcs;
        }

        public static SendCallContext<TResponse> Create(TaskCompletionSource<TResponse> tcs)
        {
            return new SendCallContext<TResponse>(tcs);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool dispose)
        {
            if (disposed)
            {
                return;
            }

            try
            {
                if (dispose)
                {

                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}