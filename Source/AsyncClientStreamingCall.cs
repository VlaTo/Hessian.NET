using System;
using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public sealed class AsyncClientStreamingCall<TRequest, TResponse> : IDisposable
    {
        private readonly IClientStreamWriter<TRequest> writer;

        public Task<TResponse> ResponseAsync
        {
            get;
        }

        public AsyncClientStreamingCall(
            IClientStreamWriter<TRequest> writer,
            Task<TResponse> responseAsync,
            Action disposeAction)
        {
            if (null == writer)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            this.writer = writer;
            ResponseAsync = responseAsync;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}