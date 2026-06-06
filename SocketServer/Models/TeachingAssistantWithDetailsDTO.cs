namespace SocketServer.Models
{
    internal class TeachingAssistantWithDetailsDTO(PersonWithDetailsDTO person) : PersonWithDetailsDTO(person)
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
