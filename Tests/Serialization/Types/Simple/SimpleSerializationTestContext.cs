using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Tests.Serialization.Types.Simple
{
    public abstract class SimpleSerializationTestContext : SerializationTestContext
    {
        protected override Task ArrangeAsync() => Task.CompletedTask;
    }
}
