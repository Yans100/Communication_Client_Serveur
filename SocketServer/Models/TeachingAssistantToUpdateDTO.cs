using System.ComponentModel.DataAnnotations;

namespace SocketServer.Models
{
    internal class TeachingAssistantToUpdateDTO : PersonToUpdateDTO
    {
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
