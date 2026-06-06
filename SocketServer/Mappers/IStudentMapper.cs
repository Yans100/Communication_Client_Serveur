using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal interface IStudentMapper : IPersonMapper
    {
        IEnumerable<StudentWithDetailsDTO> MapWithDetails(IEnumerable<Student> elements);
        new StudentDTO Map(Person person);
        new Student Map(PersonToAddDTO personToAdd);
        new Student Map(PersonToUpdateDTO personToUpdate, Person person);
        new StudentWithDetailsDTO MapWithDetails(Person person);
    }
}
