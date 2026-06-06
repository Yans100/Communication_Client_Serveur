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
    internal class TeacherController
    {
        private readonly IPersonRepository personRepository;
        private readonly ITeacherMapper mapper;
        private readonly IValidationService validationService;

        public TeacherController(IPersonRepository personRepository, ITeacherMapper teacherMapper, IValidationService validationService)
        {
            this.personRepository = personRepository;
            mapper = teacherMapper;
            this.validationService = validationService;
        }

        [TCPAuthorize(Role.User, Role.Admin)]
        [TCPRoute("GetTeachers")]
        public async Task<ResponseObject<IEnumerable<TeacherWithDetailsDTO>>> GetAllAsync()
        {
            var people = await personRepository.GetPeopleOfTypeAsync<Teacher>();

            return new ResponseObject<IEnumerable<TeacherWithDetailsDTO>>(Enums.ResponseState.Success, mapper.MapWithDetails(people));
        }

        [TCPAuthorize(Role.User, Role.Admin)]
        [TCPRoute("GetTeachersInField")]
        public async Task<ResponseObject<IEnumerable<TeacherWithDetailsDTO>>> GetAllInFieldAsync(SockerServer.Data.Enums.BuisnessField field)
        {
            var people = await personRepository.GetPeopleOfTypeInBuisnesField<Teacher>(field);

            return new ResponseObject<IEnumerable<TeacherWithDetailsDTO>>(Enums.ResponseState.Success, mapper.MapWithDetails(people));
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("AddTeacher")]
        public async Task<ResponseObject<TeacherDTO>> AddAsync(TeacherToAddDTO personToAdd)
        {
            if (!validationService.Validate(personToAdd))
                return new ResponseObject<TeacherDTO>(ResponseState.InvalidRequest, "Invalid Property Length");

            var newPerson = await personRepository.AddAsync(mapper.Map(personToAdd));
            await personRepository.SaveChangesAsync();

            if (newPerson is null)
                return new ResponseObject<TeacherDTO>(Enums.ResponseState.InvalidRequest, "Couldn't add teacher");

            return new ResponseObject<TeacherDTO>(Enums.ResponseState.Success, mapper.Map(newPerson));
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("UpdateTeacher")]
        public async Task<ResponseObject<TeacherDTO>> UpdateAsync(TeacherToUpdateDTO personToUpdate)
        {
            if (!validationService.Validate(personToUpdate))
                return new ResponseObject<TeacherDTO>(ResponseState.InvalidRequest, "Invalid Property Length");

            var currentPerson = await personRepository.GetAsync(personToUpdate.Id);
            if (currentPerson is null)
                return new ResponseObject<TeacherDTO>(Enums.ResponseState.NotFound, "Not Found");

            var updated = personRepository.Update(mapper.Map(personToUpdate, currentPerson));
            await personRepository.SaveChangesAsync();

            return new ResponseObject<TeacherDTO>(Enums.ResponseState.Success, mapper.Map(updated));

        }
    }
}
