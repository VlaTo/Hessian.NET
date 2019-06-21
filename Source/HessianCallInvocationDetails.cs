using System.IO;
using LibraProgramming.Serialization.Hessian.Core;

namespace LibraProgramming.Serialization.Hessian
{
    /// <summary>
    /// Hessian call invocation class.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class HessianCallInvocationDetails<TRequest, TResponse> : CallInvocationDetails<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private readonly DataContractHessianSerializer serializer;
        private readonly DataContractHessianSerializer deserializer;

        /// <summary>
        /// Initialize a new instance of the <see cref="HessianCallInvocationDetails{TRequest,TResponse}" /> class.
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        public HessianCallInvocationDetails(Channel channel,
            Method<TRequest, TResponse> method,
            string host,
            CallOptions options)
            : base(channel, method, host, options)
        {
            var settings = DefaultHessianSerializerSettings.Instance;

            serializer = new DataContractHessianSerializer(typeof(TRequest), settings);
            deserializer = new DataContractHessianSerializer(typeof(TResponse), settings);
        }

        /// <inheritdoc cref="CallInvocationDetails{TRequest,TResponse}.Serialize" />
        public override byte[] Serialize(TRequest request)
        {
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, request);
                return stream.ToArray();
            }
        }

        /// <inheritdoc cref="CallInvocationDetails{TRequest,TResponse}.Deserialize" />
        public override TResponse Deserialize(byte[] payload)
        {
            using (var stream = new MemoryStream(payload))
            {
                return (TResponse) deserializer.ReadObject(stream);
            }
        }
    }
}