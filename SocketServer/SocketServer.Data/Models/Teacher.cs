using System.ComponentModel.DataAnnotations;

namespace SockerServer.Data.Models
{
    public class Teacher : Person
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public Teacher(Person person) : base(person) { }
        public Teacher() { }
    }
}
