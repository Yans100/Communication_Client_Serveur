using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal class CategoryMapper : ICategoryMapper
    {
        public CategoryDTO Map(Category element)
        {
            return new()
            {
                Name = element.Name,
                Id = (SockerServer.Data.Enums.Category)element.Id,
            };
        }
    }
}
