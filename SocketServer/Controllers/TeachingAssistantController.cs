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
    internal class TeachingAssistantController
    {
        private readonly IPersonRepository personRepository;
        private readonly ITeachingAssistantMapper mapper;
        private readonly IValidationService validationService;

        public TeachingAssistantController(IPersonRepository personRepository, ITeachingAssistantMapper teacherMapper, IValidationService validationService)
        {
            this.personRepository = personRepository;
            mapper = teacherMapper;
            this.validationService = validationService;
        }

        [TCPAuthorize(Role.User, Role.Admin)]
        [TCPRoute("GetTeachingAssistants")]
        public async Task<ResponseObject<IEnumerable<TeachingAssistantWithDetailsDTO>>> GetAllAsync()
        {
            var people = await personRepository.GetPeopleOfTypeAsync<TeachingAssistant>();

            return new ResponseObject<IEnumerable<TeachingAssistantWithDetailsDTO>>(Enums.ResponseState.Success, mapper.MapWithDetails(people));
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("AddTeachingAssitant")]
        public async Task<ResponseObject<TeachingAssistantDTO>> AddAsync(TeachingAssistantToAddDTO personToAdd)
        {
            if (!validationService.Validate(personToAdd))
                return new ResponseObject<TeachingAssistantDTO>(ResponseState.InvalidRequest, "Invalid Property Length");

            var newPerson = await personRepository.AddAsync(mapper.Map(personToAdd));
            await personRepository.SaveChangesAsync();

            if (newPerson is null)
                return new ResponseObject<TeachingAssistantDTO>(Enums.ResponseState.InvalidRequest, "Couldn't add teaching assistant");

            return new ResponseObject<TeachingAssistantDTO>(Enums.ResponseState.Success, mapper.Map(newPerson));
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("UpdateTeachingAssitant")]
        public async Task<ResponseObject<TeachingAssistantDTO>> UpdateAsync(TeachingAssistantToUpdateDTO personToUpdate)
        {
            if (!validationService.Validate(personToUpdate))
                return new ResponseObject<TeachingAssistantDTO>(ResponseState.InvalidRequest, "Invalid Property Length");

            var currentPerson = await personRepository.GetAsync(personToUpdate.Id);
            if (currentPerson is null)
                return new ResponseObject<TeachingAssistantDTO>(Enums.ResponseState.NotFound, "Not Found");

            var updated = personRepository.Update(mapper.Map(personToUpdate, currentPerson));
            await personRepository.SaveChangesAsync();

            return new ResponseObject<TeachingAssistantDTO>(Enums.ResponseState.Success, mapper.Map(updated));

        }
    }
}
