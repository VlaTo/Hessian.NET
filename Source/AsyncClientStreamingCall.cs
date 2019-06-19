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
        public AsyncClientStreamingCall(
            IClientStreamWriter<TRequest> writer,
            Task<TResponse> responseAsync,
            Action disposeAction)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}