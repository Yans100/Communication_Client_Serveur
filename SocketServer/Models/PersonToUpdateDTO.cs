using System.ComponentModel.DataAnnotations;

namespace SocketServer.Models
{
    internal class PersonToUpdateDTO
    {
        public int Id { get; set; }
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public string LastName { get; set; } = string.Empty;
        public bool IsOnRedList { get; set; }
        public int BuisnessFieldId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
    }
}
