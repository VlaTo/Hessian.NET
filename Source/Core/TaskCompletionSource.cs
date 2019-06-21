using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal class TaskCompletionSource
    {
        private readonly TaskCompletionSource<object> tcs;

        public Task Task => tcs.Task;

        public TaskCompletionSource()
        {
            tcs = new TaskCompletionSource<object>();
        }

        public void SetCanceled() => tcs.SetCanceled();

        public void SetCompleted() => tcs.SetResult(null);

        public void SetException(Exception exception) => tcs.SetException(exception);

        public bool TrySetCanceled() => tcs.TrySetCanceled();

#if (NETSTANDARD13 || NETSTANDARD20)
        public bool TrySetCanceled(CancellationToken cancellationToken) => tcs.TrySetCanceled(cancellationToken);
#endif
        public bool TrySetException(Exception exception) => tcs.TrySetException(exception);

        public bool TrySetCompleted() => tcs.TrySetResult(null);
    }
}