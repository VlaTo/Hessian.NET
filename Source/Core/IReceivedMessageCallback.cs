namespace LibraProgramming.Serialization.Hessian.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReceivedMessageCallback
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="payload"></param>
        void OnClientResponse(bool success, byte[] payload);
    }
}