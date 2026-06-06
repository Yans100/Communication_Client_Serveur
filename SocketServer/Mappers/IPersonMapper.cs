using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal interface IPersonMapper
    {
        PersonDTO Map(Person person);
        Person Map(PersonToAddDTO personToAdd);
        Person Map(PersonToUpdateDTO personToUpdate, Person person);
        PersonWithDetailsDTO MapWithDetails(Person person);
    }
}
