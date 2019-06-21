using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian.Core
{
    internal sealed class WorkingTaskQueue : ITaskQueue
    {
        private readonly IList<Task> tasks;

        public WorkingTaskQueue()
        {
            tasks = new List<Task>();
        }

        public void Add(Task task)
        {
            if (null == task)
            {
                throw new ArgumentNullException(nameof(task));
            }

            tasks.Add(task);
        }
    }
}