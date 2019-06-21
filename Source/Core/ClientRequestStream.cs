using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal class ClientRequestStream<TRequest, TResponse> : IClientStreamWriter<TRequest>
        where TRequest : class
        where TResponse : class
    {
        private readonly AsyncCall<TRequest, TResponse> call;

        public ClientRequestStream(AsyncCall<TRequest, TResponse> call)
        {
            this.call = call;
        }

        public Task WriteAsync(TRequest message)
        {
            return call.SendMessageAsync(message);
        }

        public Task CompleteAsync()
        {
            return call.SendCloseFromClientAsync();
        }
    }
}