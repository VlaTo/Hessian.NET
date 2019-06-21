using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal static class TaskEx
    {
        private static Task completedTask;

        public static Task CompletedTask
        {
            get
            {
                if (null == completedTask)
                {
#if NET45
                    completedTask = Task.FromResult(true);
#else
                    completedTask = Task.CompletedTask;
#endif
                }

                return completedTask;
            }
        }
    }
}