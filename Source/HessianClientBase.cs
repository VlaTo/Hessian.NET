using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class HessianClientBase
    {
        protected CallInvoker Invoker
        {
            get;
        }

        protected HessianClientBase(CallInvoker invoker)
        {
            if (null == invoker)
            {
                throw new ArgumentNullException(nameof(invoker));
            }

            Invoker = invoker;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class HessianClientBase<T> : HessianClientBase
        where T : HessianClientBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        protected HessianClientBase(Channel channel)
            : this(new DefaultCallInvoker(channel))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoker"></param>
        protected HessianClientBase(CallInvoker invoker)
            : base(invoker)
        {
        }
    }
}