using System;

namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    internal sealed class DisposeAction<TContext> : IDisposable
    {
        private readonly Action<TContext> action;
        private bool disposed;

        /// <summary>
        /// 
        /// </summary>
        public TContext Context
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public DisposeAction(TContext context, Action<TContext> action)
        {
            this.action = action;
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
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
                    action.Invoke(Context);
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}