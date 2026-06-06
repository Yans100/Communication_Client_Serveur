namespace SocketServer.Models
{
    internal class StudentWithDetailsDTO(PersonWithDetailsDTO dto) : PersonWithDetailsDTO(dto)
    {
        public string ReferenceNumber { get; set; } = string.Empty;
    }
}
