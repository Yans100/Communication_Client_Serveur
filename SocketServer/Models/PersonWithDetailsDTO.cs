namespace SocketServer.Models
{
    internal class PersonWithDetailsDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsOnRedList { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public CategoryDTO Category { get; set; } = default!;
        public BuisnessFieldDTO BuisnessField { get; set; } = default!;

        public PersonWithDetailsDTO(PersonWithDetailsDTO personDTO)
        {
            Id = personDTO.Id;
            FirstName = personDTO.FirstName;
            LastName = personDTO.LastName;
            IsOnRedList = personDTO.IsOnRedList;
            EmailAddress = personDTO.EmailAddress;
            BuisnessField = personDTO.BuisnessField;
            Category = personDTO.Category;
        }

        public PersonWithDetailsDTO() { }
    }
}
