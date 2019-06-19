using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    public interface IAsyncStreamWriter<in T>
    {
        Task WriteAsync(T message);
    }
}