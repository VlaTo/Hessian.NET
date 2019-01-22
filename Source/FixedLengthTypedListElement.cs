using System;
using System.Collections;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FixedLengthTypedListElement : ISerializationElement
    {
        /// <inheritdoc cref="ISerializationElement.ObjectType" />
        public Type ObjectType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISerializationElement Element
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="element"></param>
        public FixedLengthTypedListElement(Type objectType, ISerializationElement element)
        {
            ObjectType = objectType;
            Element = element;
        }

        /// <inheritdoc cref="ISerializationElement.Serialize" />
        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            if (null == graph)
            {
                writer.WriteNull();
                return;
            }

            var index = context.Instances.IndexOf(graph);

            if (index > -1)
            {
                writer.WriteInstanceReference(index);
                return;
            }

            context.Instances.Add(graph);

            index = context.Classes.IndexOf(ObjectType);

            if (0 > index)
            {

            }

            var collection = (ICollection) graph;

            using (writer.BeginFixedArray(ObjectType.Name, collection.Count))
            {
                foreach (var item in collection)
                {
                    Element.Serialize(writer, item, context);
                }
            }
        }

        /// <inheritdoc cref="ISerializationElement.Deserialize" />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}