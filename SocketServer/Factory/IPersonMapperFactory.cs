using SockerServer.Data.Enums;
using SocketServer.Mappers;

namespace SocketServer.Factory
{
    internal interface IPersonMapperFactory
    {
        IPersonMapper GetMapper(Category category);
    }
}
