using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class VariableLengthTypedArrayElement : ISerializationElement
    {
        /// <inheritdoc cref="ISerializationElement.ObjectType" />
        public Type ObjectType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public VariableLengthTypedArrayElement(Type genericType)
        {
        }

        /// <inheritdoc cref="ISerializationElement.Serialize" />
        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc cref="ISerializationElement.Deserialize" />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}