namespace SocketServer.Models
{
    internal class TeachingAssistantDTO(PersonDTO personDTO) : PersonDTO(personDTO)
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
