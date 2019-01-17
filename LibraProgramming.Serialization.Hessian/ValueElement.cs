using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public class ValueElement : ISerializationElement
    {
        /// <inheritdoc cref="ISerializationElement.ObjectType" />
        public Type ObjectType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public IObjectSerializer ObjectSerializer
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="objectSerializer"></param>
        public ValueElement(Type objectType, IObjectSerializer objectSerializer)
        {
            ObjectType = objectType;
            ObjectSerializer = objectSerializer;
        }

        /// <inheritdoc cref="ISerializationElement.Serialize" />
        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            ObjectSerializer.Serialize(writer, graph);
        }

        /// <inheritdoc cref="ISerializationElement.Deserialize" />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            return ObjectSerializer.Deserialize(reader);
        }
    }
}