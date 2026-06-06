using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal class BuisnessFieldMapper : IBuisnessFieldMapper
    {
        public BuisnessFieldDTO Map(BuisnessField element)
        {
            return new()
            {
                Name = element.Name,
                Id = (SockerServer.Data.Enums.BuisnessField)element.Id,
            };
        }
    }
}
