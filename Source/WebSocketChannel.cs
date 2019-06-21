using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace LibraProgramming.Serialization.Hessian
{
    public sealed class WebSocketChannel : Channel
    {
        private WebSocket socket;

        public WebSocketChannel()
        {
        }

        public override Task SendAsync(byte[] payload)
        {
            var array = new ArraySegment<byte>(payload);
            return socket.SendAsync(array, WebSocketMessageType.Binary, true, default);
        }

        public override Task<byte[]> ReceiveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}