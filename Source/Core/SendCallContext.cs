using System;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal class SendCallContext : IDisposable
    {
        private bool disposed;

        private SendCallContext()
        {
        }

        public static SendCallContext Create()
        {
            return new SendCallContext();
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