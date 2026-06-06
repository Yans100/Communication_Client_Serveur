using System.ComponentModel.DataAnnotations;

namespace SocketServer.Models
{
    internal class PersonToAddDTO
    {
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public required string FirstName { get; set; }
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public required string LastName { get; set; }
        public required bool IsOnRedList { get; set; }
        public required int BuisnessFieldId { get; set; }
        [StringLength(25, ErrorMessage = "Longueur minimal de 3 et max de 25", MinimumLength = 3)]
        public required string Password { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
    }
}
