using System;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISerializationElement
    {
        /// <summary>
        /// 
        /// </summary>
        Type ObjectType
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="graph"></param>
        /// <param name="context"></param>
        void Serialize(HessianOutputWriter writer, object graph, HessianSerializationContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        object Deserialize(HessianInputReader reader, HessianSerializationContext context);
    }
}