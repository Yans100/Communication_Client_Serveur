using System.ComponentModel.DataAnnotations;

namespace SockerServer.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsOnRedList { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public int BuisnessFieldId { get; set; }
        public BuisnessField BuisnessField { get; set; } = default!;

        public Person(Person person)
        {
            this.Id = person.Id;
            this.LastName = person.LastName;
            this.FirstName = person.FirstName;
            this.EmailAddress = person.EmailAddress;
            this.IsOnRedList = person.IsOnRedList;
            this.CategoryId = person.CategoryId;
            this.Category = person.Category;
            this.BuisnessField = person.BuisnessField;
            this.BuisnessFieldId = person.BuisnessFieldId;
            this.Password = person.Password;
        }

        public Person() { }
    }
}
