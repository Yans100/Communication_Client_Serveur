using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal interface IBuisnessFieldMapper
    {
        BuisnessFieldDTO Map(BuisnessField element);
    }
}
