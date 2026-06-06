using SockerServer.Data.Enums;
using SocketServer.Mappers;

namespace SocketServer.Factory
{
    internal class PersonMapperFactory : IPersonMapperFactory
    {
        private readonly IStudentMapper studentMapper;
        private readonly ITeacherMapper teacherMapper;
        private readonly ITeachingAssistantMapper teachingAssistantMapper;

        public PersonMapperFactory(IStudentMapper studentMapper, ITeacherMapper teacherMapper, ITeachingAssistantMapper teachingAssistantMapper)
        {
            this.teacherMapper = teacherMapper;
            this.studentMapper = studentMapper;
            this.teachingAssistantMapper = teachingAssistantMapper;
        }

        public IPersonMapper GetMapper(Category category) => category switch
        {
            Category.Student => studentMapper,
            Category.Teacher => teacherMapper,
            Category.TeachingAssistant => teachingAssistantMapper,
            _ => throw new NotImplementedException(),
        };
    }
}
