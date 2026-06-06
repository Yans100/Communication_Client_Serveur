namespace SocketServer.Models
{
    internal class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsOnRedList { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public int BuisnessFieldId { get; set; }

        public PersonDTO(PersonDTO personDTO)
        {
            Id = personDTO.Id;
            FirstName = personDTO.FirstName;
            LastName = personDTO.LastName;
            IsOnRedList = personDTO.IsOnRedList;
            EmailAddress = personDTO.EmailAddress;
            BuisnessFieldId = personDTO.BuisnessFieldId;
        }

        public PersonDTO() { }
    }
}