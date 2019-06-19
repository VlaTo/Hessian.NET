using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ClientBase
    {
        protected Channel Channel
        {
            get;
        }

        protected CallInvoker Invoker
        {
            get;
        }

        protected ClientBase(Channel channel, CallInvoker invoker)
        {
            if (null == channel)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            if (null == invoker)
            {
                throw new ArgumentNullException(nameof(invoker));
            }

            Channel = channel;
            Invoker = invoker;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ClientBase<T> : ClientBase
        where T : ClientBase<T>
    {
        protected ClientBase(Channel channel)
            : this(channel, null)
        {
        }

        protected ClientBase(Channel channel, CallInvoker invoker)
            : base(channel, invoker)
        {
        }
    }
}