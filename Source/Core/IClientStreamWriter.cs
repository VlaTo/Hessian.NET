using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    public interface IClientStreamWriter<in T> : IAsyncStreamWriter<T>
    {
        Task CompleteAsync();
    }
}