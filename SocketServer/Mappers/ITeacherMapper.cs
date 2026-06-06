using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal interface ITeacherMapper : IPersonMapper
    {
        IEnumerable<TeacherWithDetailsDTO> MapWithDetails(IEnumerable<Teacher> elements);
        new TeacherDTO Map(Person person);
        new Teacher Map(PersonToAddDTO personToAdd);
        new Teacher Map(PersonToUpdateDTO personToUpdate, Person person);
        new TeacherWithDetailsDTO MapWithDetails(Person person);
    }
}
