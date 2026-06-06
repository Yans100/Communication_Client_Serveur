using SockerServer.Data.Enums;
using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal class StudentMapper(IBuisnessFieldMapper bfm, ICategoryMapper cm) : PersonMapper(bfm, cm), IStudentMapper
    {
        public override StudentDTO Map(Person person)
        {
            Student? student = person as Student ?? throw new ArgumentNullException(nameof(person));

            StudentDTO studentDTO = new(base.Map(person));

            studentDTO.ReferenceNumber = student.ReferenceNumber;

            return studentDTO;
        }

        public override Student Map(PersonToAddDTO personToAdd)
        {
            StudentToAddDTO studentToAddDTO = personToAdd as StudentToAddDTO ?? throw new ArgumentNullException(nameof(personToAdd));

            Student student = new(base.Map(personToAdd))
            {
                ReferenceNumber = studentToAddDTO.ReferenceNumber,
                CategoryId = (int)SockerServer.Data.Enums.Category.Student,
                BuisnessFieldId = studentToAddDTO.BuisnessFieldId,
            };

            return student;
        }

        public override Student Map(PersonToUpdateDTO personToUpdate, Person currPerson)
        {
            StudentToUpdateDTO studentToUpdateDTO = personToUpdate as StudentToUpdateDTO ?? throw new ArgumentNullException(nameof(personToUpdate));
            Student currentStudent = currPerson as Student ?? throw new ArgumentNullException(nameof(currPerson));

            Student student = new(base.Map(personToUpdate, currPerson))
            {
                ReferenceNumber = currentStudent.ReferenceNumber,
                CategoryId = (int)SockerServer.Data.Enums.Category.Student,
            };

            if (!student.IsOnRedList)
            {
                student.ReferenceNumber = studentToUpdateDTO.ReferenceNumber;
            }

            return student;
        }

        public IEnumerable<StudentWithDetailsDTO> MapWithDetails(IEnumerable<Student> elements) => elements.Select(MapWithDetails);

        public override StudentWithDetailsDTO MapWithDetails(Person person)
        {
            Student student = person as Student ?? throw new ArgumentNullException(nameof(person));

            StudentWithDetailsDTO studentWithDetails = new(base.MapWithDetails(student))
            {
                ReferenceNumber = student.ReferenceNumber,
            };

            return studentWithDetails;
        }
    }
}
