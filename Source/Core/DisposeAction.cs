using System;

namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    internal sealed class DisposeAction<TContext> : IDisposable
    {
        private readonly TContext context;
        private readonly Action<DisposeAction<TContext>, TContext> action;
        private bool disposed;

        public DisposeAction(TContext context, Action<DisposeAction<TContext>, TContext> action)
        {
            this.context = context;
            this.action = action;
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
                    action.Invoke(this, context);
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}