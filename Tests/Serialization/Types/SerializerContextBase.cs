using System;
using System.IO;
using LibraProgramming.Serialization.Hessian;

namespace LibraProgramming.Serialization.Tests.Serialization.Types
{
    public abstract class SerializerContextBase : TestContextBase
    {
        protected byte[] Output { get; private set; }

        protected abstract object Data { get; }

        protected DataContractHessianSerializer Serializer { get; private set; }

        protected override void Arrange()
        {
            Serializer = new DataContractHessianSerializer(Data.GetType(), settings: null);
        }

        protected override void Act()
        {
            using (var stream = new MemoryStream())
            {
                Serializer.WriteObject(stream, Data);
                Output = stream.ToArray();
            }

            DebugWriteArray(Output);
        }
    }
}