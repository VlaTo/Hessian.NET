using System;

namespace LibraProgramming.Hessian
{
    public interface IObjectSerializerFactory
    {
        IObjectSerializer GetSerializer(Type target);
    }
}