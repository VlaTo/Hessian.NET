using System;
using System.Collections;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TypedCollectionElement : ISerializationElement
    {
        /// <inheritdoc cref="ISerializationElement.ObjectType" />
        public Type ObjectType
        {
            get;
        }

        public ISerializationElement Element
        {
            get;
        }

        public TypedCollectionElement(ISerializationElement element)
        {
            Element = element;
            ObjectType = typeof(ICollection).MakeGenericType(Element.ObjectType);
        }

        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}