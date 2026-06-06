namespace SocketServer.Models
{
    internal class TeacherDTO(PersonDTO person) : PersonDTO(person)
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
