using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Channel
    {
        public abstract Task SendAsync(byte[] payload);

        public abstract Task<byte[]> ReceiveAsync();
    }
}