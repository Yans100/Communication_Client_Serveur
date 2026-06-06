using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal interface ICategoryMapper
    {
        CategoryDTO Map(Category element);
    }
}
