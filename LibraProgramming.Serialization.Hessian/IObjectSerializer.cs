namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="graph"></param>
        void Serialize(HessianOutputWriter writer, object graph);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        object Deserialize(HessianInputReader reader);
    }
}