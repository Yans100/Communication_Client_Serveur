using SockerServer.Data.Enums;

namespace SocketServer.Models
{
    internal class CategoryDTO
    {
        public required Category Id { get; set; }
        public required string Name { get; set; }
    }
}
