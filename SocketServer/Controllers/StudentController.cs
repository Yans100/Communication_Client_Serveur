using SockerServer.Data.Models;
using SockerServer.Data.Repositories;
using SocketServer.Attributes;
using SocketServer.Enums;
using SocketServer.Mappers;
using SocketServer.Models;
using SocketServer.Services;

namespace SocketServer.Controllers
{
    [TCPController]
    internal class StudentController
    {
        private readonly IPersonRepository personRepository;
        private readonly IStudentMapper mapper;
        private readonly IValidationService validationService;

        public StudentController(IPersonRepository personRepository, IStudentMapper studentMapper, IValidationService validationService)
        {
            this.personRepository = personRepository;
            this.mapper = studentMapper;
            this.validationService = validationService;
        }

        [TCPAuthorize(Role.User, Role.Admin)]
        [TCPRoute("GetStudents")]
        public async Task<ResponseObject<IEnumerable<StudentWithDetailsDTO>>> GetAllAsync()
        {
            var people = await personRepository.GetPeopleOfTypeAsync<Student>();

            return new ResponseObject<IEnumerable<StudentWithDetailsDTO>>(Enums.ResponseState.Success, mapper.MapWithDetails(people));
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("AddStudent")]
        public async Task<ResponseObject<StudentDTO>> AddAsync(StudentToAddDTO personToAdd)
        {
            if (!validationService.Validate(personToAdd))
                return new ResponseObject<StudentDTO>(ResponseState.InvalidRequest, "Invalid Property Length");

            var newPerson = await personRepository.AddAsync(mapper.Map(personToAdd));
            await personRepository.SaveChangesAsync();

            if (newPerson is null)
                return new ResponseObject<StudentDTO>(Enums.ResponseState.InvalidRequest, "Couldn't add student");

            return new ResponseObject<StudentDTO>(Enums.ResponseState.Success, mapper.Map(newPerson));
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("UpdateStudent")]
        public async Task<ResponseObject<StudentDTO>> UpdateAsync(StudentToUpdateDTO personToUpdate)
        {
            if (!validationService.Validate(personToUpdate))
                return new ResponseObject<StudentDTO>(ResponseState.InvalidRequest, "Invalid Property Length");

            var currentPerson = await personRepository.GetAsync(personToUpdate.Id);
            if (currentPerson is null)
                return new ResponseObject<StudentDTO>(Enums.ResponseState.NotFound, "Not Found");

            var updated = personRepository.Update(mapper.Map(personToUpdate, currentPerson));
            await personRepository.SaveChangesAsync();

            return new ResponseObject<StudentDTO>(Enums.ResponseState.Success, mapper.Map(updated));

        }
    }
}
