using System.IO;

namespace LibraProgramming.Serialization.Hessian
{
    public class HessianCallInvocationDetails<TRequest, TResponse>:CallInvocationDetails<TRequest,TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly DataContractHessianSerializer serializer;

        public HessianCallInvocationDetails(Channel channel,
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options)
            : base(channel, method, host, options)
        {
            serializer = new DataContractHessianSerializer(typeof(TRequest));
        }

        public override byte[] Serialize(TRequest request)
        {
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, request);
                return stream.ToArray();
            }
        }

        public override TResponse Deserialize(byte[] payload)
        {
            throw new System.NotImplementedException();
        }
    }
}