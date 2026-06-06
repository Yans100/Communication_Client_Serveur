using SockerServer.Data.Models;
using SocketServer.Models;

namespace SocketServer.Mappers
{
    internal class TeachingAssistantMapper(IBuisnessFieldMapper bfm, ICategoryMapper cm) : PersonMapper(bfm, cm), ITeachingAssistantMapper
    {
        public override TeachingAssistantDTO Map(Person person)
        {
            TeachingAssistant? teachingAssistant = person as TeachingAssistant ?? throw new ArgumentNullException(nameof(person));

            TeachingAssistantDTO teachingAssistantDTO = new(base.Map(person));

            teachingAssistantDTO.PhoneNumber = teachingAssistant.PhoneNumber;

            return teachingAssistantDTO;
        }

        public override TeachingAssistant Map(PersonToAddDTO personToAdd)
        {
            TeachingAssistantToAddDTO teachingAssistantToAddDTO = personToAdd as TeachingAssistantToAddDTO ?? throw new ArgumentNullException(nameof(personToAdd));

            TeachingAssistant teachingAssistant = new(base.Map(personToAdd))
            {
                PhoneNumber = teachingAssistantToAddDTO.PhoneNumber,
                CategoryId = (int)SockerServer.Data.Enums.Category.TeachingAssistant,
                BuisnessFieldId = teachingAssistantToAddDTO.BuisnessFieldId,
            };

            return teachingAssistant;
        }

        public override TeachingAssistant Map(PersonToUpdateDTO personToUpdate, Person currPerson)
        {
            TeachingAssistantToUpdateDTO teachingAssistantToUpdateDTO = personToUpdate as TeachingAssistantToUpdateDTO ?? throw new ArgumentNullException(nameof(personToUpdate));
            TeachingAssistant currentTeachingAssistant = currPerson as TeachingAssistant ?? throw new ArgumentNullException(nameof(currPerson));

            TeachingAssistant teachingAssistant = new(base.Map(personToUpdate, currPerson))
            {
                PhoneNumber = currentTeachingAssistant.PhoneNumber,
                CategoryId = (int)SockerServer.Data.Enums.Category.TeachingAssistant
            };

            if (!teachingAssistant.IsOnRedList)
            {
                teachingAssistant.PhoneNumber = teachingAssistantToUpdateDTO.PhoneNumber;
            }

            return teachingAssistant;
        }

        public IEnumerable<TeachingAssistantWithDetailsDTO> MapWithDetails(IEnumerable<TeachingAssistant> elements) => elements.Select(MapWithDetails);

        public override TeachingAssistantWithDetailsDTO MapWithDetails(Person person)
        {
            TeachingAssistant teachingAssistant = person as TeachingAssistant ?? throw new ArgumentNullException(nameof(person));

            TeachingAssistantWithDetailsDTO teachingAssistantWithDetails = new(base.MapWithDetails(teachingAssistant))
            {
                PhoneNumber = teachingAssistant.PhoneNumber,
            };

            return teachingAssistantWithDetails;
        }
    }
}
