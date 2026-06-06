namespace SocketServer.Models
{
    internal class StudentDTO(PersonDTO personDTO) : PersonDTO(personDTO)
    {
        public string ReferenceNumber { get; set; } = string.Empty;
    }
}
