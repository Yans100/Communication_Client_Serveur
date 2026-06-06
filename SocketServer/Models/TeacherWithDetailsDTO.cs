namespace SocketServer.Models
{
    internal class TeacherWithDetailsDTO(PersonWithDetailsDTO dto) : PersonWithDetailsDTO(dto)
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
