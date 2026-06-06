using System.ComponentModel.DataAnnotations;

namespace SockerServer.Data.Models
{
    public class Student : Person
    {
        public string ReferenceNumber { get; set; } = string.Empty;

        public Student(Person person) : base(person) { }

        public Student() { }
    }
}
