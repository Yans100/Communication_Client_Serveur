using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal abstract class PersonMapper : IPersonMapper
    {
        private readonly IBuisnessFieldMapper buisnessFieldMapper;
        private readonly ICategoryMapper categoryMapper;

        protected PersonMapper(IBuisnessFieldMapper buisnessFieldMapper, ICategoryMapper categoryMapper)
        {
            this.buisnessFieldMapper = buisnessFieldMapper;
            this.categoryMapper = categoryMapper;
        }

        public virtual PersonDTO Map(Person person)
        {
            PersonDTO newPerson = new()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsOnRedList = person.IsOnRedList,
                EmailAddress = person.EmailAddress,
                BuisnessFieldId = person.BuisnessFieldId,
            };

            return newPerson;
        }

        public virtual Person Map(PersonToAddDTO personToAdd)
        {  
            Person person = new()
            {
                EmailAddress = personToAdd.EmailAddress,
                FirstName = personToAdd.FirstName,
                LastName = personToAdd.LastName,
                IsOnRedList = personToAdd.IsOnRedList,
                BuisnessFieldId = personToAdd.BuisnessFieldId,
                Password = personToAdd.Password,
            };

            return person;
        }

        public virtual Person Map(PersonToUpdateDTO personToUpdate, Person currPerson)
        {
            Person person = new()
            {
                Id = personToUpdate.Id,
                FirstName = personToUpdate.FirstName,
                LastName = personToUpdate.LastName,
                IsOnRedList = personToUpdate.IsOnRedList,
                BuisnessFieldId = currPerson.BuisnessFieldId,
                EmailAddress = currPerson.EmailAddress,
                Password = currPerson.Password,
            };

            if (!personToUpdate.IsOnRedList)
            {
                person.BuisnessFieldId = personToUpdate.BuisnessFieldId;
                person.EmailAddress = personToUpdate.EmailAddress;
            }

            return person;
        }

        public virtual PersonWithDetailsDTO MapWithDetails(Person person)
        {
            PersonWithDetailsDTO redPerson = new()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Category = categoryMapper.Map(person.Category),
                BuisnessField = buisnessFieldMapper.Map(person.BuisnessField),
                IsOnRedList = person.IsOnRedList,
            };

            if (!person.IsOnRedList)
            {
                redPerson.EmailAddress = person.EmailAddress;
            }

            return redPerson;
        }

    }
}
