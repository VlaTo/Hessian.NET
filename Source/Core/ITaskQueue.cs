using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    public interface ITaskQueue
    {
        void Add(Task task);
    }
}