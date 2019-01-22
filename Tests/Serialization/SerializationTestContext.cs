using System;
using System.IO;
using LibraProgramming.Serialization.Hessian;

namespace LibraProgramming.Serialization.Tests.Serialization
{
    public abstract class SerializationTestContext : TestContextBase
    {
        protected byte[] Data { get; private set; }

        protected abstract Action<HessianOutputWriter> Action { get; }

        protected override void Act()
        {
            var stream = new MemoryStream();

            using (var writer = new HessianOutputWriter(stream))
            {
                Action.Invoke(writer);
                Data = stream.ToArray();
            }

            DebugWriteArray(Data);
        }
    }
}