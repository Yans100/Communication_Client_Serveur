using SocketServer.Enums;

namespace SocketServer.Models
{
    public interface IResponseObject
    {
        public ResponseState ResponseState { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
