using System;
using System.IO;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DisposableStreamHandler : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// 
        /// </summary>
        protected Stream Stream
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        protected DisposableStreamHandler(Stream stream)
        {
            Stream = stream;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
            if (!disposed)
            {
                try
                {
                    if (dispose)
                    {
                        Stream.Dispose();
                        Stream = null;
                    }
                }
                finally
                {
                    disposed = true;
                }
            }
        }
    }
}