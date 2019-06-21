using System.Diagnostics;
using System.Threading.Tasks;
using LibraProgramming.Serialization.Hessian;

namespace LibraProgramming.Serialization.Tests.Client
{
    internal class TestChannel : Channel
    {
        public override Task SendAsync(byte[] payload)
        {
            Debug.WriteLine($"[TestChannel.SendAsync] Sending bytes: {payload.Length}");
            return Task.CompletedTask;
        }

        public override Task<byte[]> ReceiveAsync()
        {
            Debug.WriteLine("[TestChannel.ReceiveAsync] receive complete");
            return Task.FromResult(new byte[0]);
        }
    }
}