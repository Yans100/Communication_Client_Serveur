using System.Net.Sockets;
using System.Text;

namespace SocketServer.Models
{
    internal class StateObject : IStateObject
    {
        private const int bufferSize = 1024;
        public int Id { get; init; }
        public Socket Socket { get; init; } = default!;
        public byte[] Buffer { get; } = new byte[bufferSize];
        public StringBuilder StringBuider { get; } = new StringBuilder();
        public int BufferSize => bufferSize;
        public bool Close { get; set; } = false;
    }
}
