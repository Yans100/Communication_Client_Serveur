using SockerServer.Data.Enums;
using SockerServer.Data.Repositories;
using SocketServer.Attributes;
using SocketServer.Enums;
using SocketServer.Factory;
using SocketServer.Models;

namespace SocketServer.Controllers
{
    [TCPController]
    internal class PersonController
    {
        private readonly IPersonRepository personRepository;
        private readonly IPersonMapperFactory personMapperFactory;

        public PersonController(IPersonRepository personRepository, IPersonMapperFactory personMapperFactory)
        {
            this.personRepository = personRepository;
            this.personMapperFactory = personMapperFactory;
        }

        [TCPAuthorize(Role.User, Role.Admin)]
        [TCPRoute("GetPerson")]
        public async Task<ResponseObject<PersonWithDetailsDTO>> GetAsync(int id)
        {
            var person = await personRepository.GetWithDetailsAsync(id);
            if (person is not null) 
                return new ResponseObject<PersonWithDetailsDTO>(Enums.ResponseState.Success, personMapperFactory.GetMapper((Category)person.CategoryId).MapWithDetails(person));

            return new ResponseObject<PersonWithDetailsDTO>(Enums.ResponseState.NotFound, "Not Found");
        }

        [TCPAuthorize(Role.Admin)]
        [TCPRoute("RemovePerson")]
        public async Task<ResponseObject> RemoveAsync(int id)
        {
            var person = await personRepository.GetAsync(id);
            if (person is null)
                return new ResponseObject(ResponseState.NotFound, "Not found");

            personRepository.Remove(person);
            await personRepository.SaveChangesAsync();

            return new ResponseObject(ResponseState.Success, "Success");
        }
    }
}
