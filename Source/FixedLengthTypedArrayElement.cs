using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FixedLengthTypedArrayElement : ISerializationElement
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
        /// <param name="element"></param>
        public FixedLengthTypedArrayElement(ISerializationElement element)
        {
            Element = element;
            ObjectType = element.ObjectType.MakeArrayType(1);
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

            WriteArray(writer, (Array) graph, context);
        }

        /// <inheritdoc cref="ISerializationElement.Deserialize" />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            throw new NotImplementedException();
        }

        private void WriteArray(HessianOutputWriter writer, Array array, HessianSerializationContext context)
        {
            using (writer.BeginFixedArray(ObjectType.Name, array.Length))
            {
                var index = array.GetLowerBound(0);
                var end = array.GetUpperBound(0);

                for (; index <= end; index++)
                {
                    var item = array.GetValue(index);
                    Element.Serialize(writer, item, context);
                }
            }
        }
    }
}