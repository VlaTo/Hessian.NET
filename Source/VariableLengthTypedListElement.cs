using System;
using System.Collections;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class VariableLengthTypedListElement : ISerializationElement
    {
        /// <summary>
        /// 
        /// </summary>
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
        public VariableLengthTypedListElement(Type objectType, ISerializationElement element)
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

            using (writer.BeginArray(ObjectType.Name))
            {
                foreach (var item in (IEnumerable) graph)
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