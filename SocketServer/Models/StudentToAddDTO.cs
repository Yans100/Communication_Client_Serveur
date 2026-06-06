using System.ComponentModel.DataAnnotations;

namespace SocketServer.Models
{
    internal class StudentToAddDTO : PersonToAddDTO
    {
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public string ReferenceNumber { get; set; } = string.Empty;
    }
}
