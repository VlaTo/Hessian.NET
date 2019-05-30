using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BinaryDataElement : ISerializationElement
    {
        /// <inheritdoc />
        public Type ObjectType => typeof(byte);

        /// <inheritdoc />
        public void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context)
        {
            if (null == graph)
            {
                writer.WriteNull();
                return;
            }

            /*var index = context.Instances.IndexOf(graph);

            if (index > -1)
            {
                writer.WriteInstanceReference(index);
                return;
            }

            context.Instances.Add(graph);*/

            writer.WriteBytes((byte[]) graph);
        }

        /// <inheritdoc />
        public object Deserialize(HessianInputReader reader, HessianSerializationContext context)
        {
            return reader.ReadBytes();
        }
    }
}