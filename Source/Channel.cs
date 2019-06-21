using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Channel
    {
        public Task SendAsync(byte[] payload)
        {
            return TaskEx.CompletedTask;
        }

        public Task<byte[]> ReceiveAsync()
        {
            return Task.FromResult(new byte[0]);
        }
    }
}