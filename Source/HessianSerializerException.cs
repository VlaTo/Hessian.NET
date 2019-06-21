using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HessianSerializerException : Exception
    {
        public HessianSerializerException()
        {
        }

        public HessianSerializerException(string message)
            : base(message)
        {
        }
    }
}