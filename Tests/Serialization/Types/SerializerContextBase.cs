using LibraProgramming.Serialization.Hessian;
using System.IO;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Tests.Serialization.Types
{
    public abstract class SerializerContextBase : TestContextBase
    {
        protected byte[] Output { get; private set; }

        protected abstract object Data { get; }

        protected DataContractHessianSerializer Serializer { get; private set; }

        protected override Task ArrangeAsync()
        {
            Serializer = new DataContractHessianSerializer(Data.GetType(), settings: null);
            return Task.CompletedTask;
        }

        protected override Task ActAsync()
        {
            using (var stream = new MemoryStream())
            {
                Serializer.WriteObject(stream, Data);
                Output = stream.ToArray();
            }

            DebugWriteArray(Output);

            return Task.CompletedTask;
        }
    }
}