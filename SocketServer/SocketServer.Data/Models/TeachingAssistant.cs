using System.ComponentModel.DataAnnotations;

namespace SockerServer.Data.Models
{
    public class TeachingAssistant : Person
    {
        [Length(3, 25, ErrorMessage = "Longueur minimal de 3 et max de 25")]
        public string PhoneNumber { get; set; } = string.Empty;

        public TeachingAssistant(Person person) : base(person) { }
        public TeachingAssistant() { }
    }
}
