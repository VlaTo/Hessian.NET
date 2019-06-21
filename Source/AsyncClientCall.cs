using System;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian
{
    public sealed class AsyncClientCall<TResponse> : IDisposable
    {
        public Task<TResponse> ResponseAsync
        {
            get;
        }

        public AsyncClientCall(
            Task<TResponse> responseAsync,
            Action disposeAction)
        {
            if (null == responseAsync)
            {
                throw new ArgumentNullException(nameof(responseAsync));
            }

            ResponseAsync = responseAsync;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}