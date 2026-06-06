using System.Net.Sockets;
using System.Text;

namespace SocketServer.Models
{
    public interface IStateObject
    {
        int Id { get; init; }
        Socket Socket { get; init; }
        byte[] Buffer { get; }
        StringBuilder StringBuider { get; }
        public int BufferSize { get; }
        public bool Close {  get; set; }
    }
}
