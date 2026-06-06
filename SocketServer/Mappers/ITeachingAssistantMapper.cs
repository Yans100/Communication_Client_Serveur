using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal interface ITeachingAssistantMapper : IPersonMapper
    {
        IEnumerable<TeachingAssistantWithDetailsDTO> MapWithDetails(IEnumerable<TeachingAssistant> elements);
        new TeachingAssistantDTO Map(Person person);
        new TeachingAssistant Map(PersonToAddDTO personToAdd);
        new TeachingAssistant Map(PersonToUpdateDTO personToUpdate, Person person);
        new TeachingAssistantWithDetailsDTO MapWithDetails(Person person);
    }
}
