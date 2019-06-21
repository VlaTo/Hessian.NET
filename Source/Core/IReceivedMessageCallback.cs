using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReceivedMessageCallback<TResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="tcs"></param>
        /// <param name="payload"></param>
        void OnClientResponse(bool success, TaskCompletionSource<TResponse> tcs, byte[] payload);
    }
}