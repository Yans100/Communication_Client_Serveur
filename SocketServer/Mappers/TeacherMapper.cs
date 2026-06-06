using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal class TeacherMapper(IBuisnessFieldMapper bfm, ICategoryMapper cm) : PersonMapper(bfm, cm), ITeacherMapper
    {
        public override TeacherDTO Map(Person person)
        {
            Teacher? teacher = person as Teacher ?? throw new ArgumentNullException(nameof(person));

            TeacherDTO teacherDTO = new(base.Map(person));

            teacherDTO.PhoneNumber = teacher.PhoneNumber;

            return teacherDTO;
        }

        public override Teacher Map(PersonToAddDTO personToAdd)
        {
            TeacherToAddDTO teacherToAddDTO = personToAdd as TeacherToAddDTO ?? throw new ArgumentNullException(nameof(personToAdd));

            Teacher teacher = new(base.Map(personToAdd))
            {
                PhoneNumber = teacherToAddDTO.PhoneNumber,
                CategoryId = (int)SockerServer.Data.Enums.Category.Teacher,
                BuisnessFieldId = teacherToAddDTO.BuisnessFieldId,
            };

            return teacher;
        }

        public override Teacher Map(PersonToUpdateDTO personToUpdate, Person currPerson)
        {
            TeacherToUpdateDTO teacherToUpdateDTO = personToUpdate as TeacherToUpdateDTO ?? throw new ArgumentNullException(nameof(personToUpdate));
            Teacher currTeacher = currPerson as Teacher ?? throw new ArgumentNullException(nameof(currPerson));

            Teacher teacher = new(base.Map(personToUpdate, currPerson))
            {
                PhoneNumber = currTeacher.PhoneNumber,
                CategoryId = (int)SockerServer.Data.Enums.Category.Teacher
            };

            if (!teacher.IsOnRedList)
            {
                teacher.PhoneNumber = teacherToUpdateDTO.PhoneNumber;        
            }

            return teacher;
        }

        public IEnumerable<TeacherWithDetailsDTO> MapWithDetails(IEnumerable<Teacher> elements) => elements.Select(MapWithDetails);

        public override TeacherWithDetailsDTO MapWithDetails(Person person)
        {
            Teacher teacher = person as Teacher ?? throw new ArgumentNullException(nameof(person));

            TeacherWithDetailsDTO teacherWithDetails = new(base.MapWithDetails(teacher))
            {
                PhoneNumber = teacher.PhoneNumber,
            };

            return teacherWithDetails;
        }
    }
}
