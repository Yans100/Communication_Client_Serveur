using Microsoft.EntityFrameworkCore;
using SockerServer.Data.Models;

namespace SockerServer.Data.Context
{
    internal static class SocketServerFluentEntities
    {
        public static ModelBuilder AddPerson(this ModelBuilder builder)
        {
            var b = builder.Entity<Person>();
            
            b.HasDiscriminator(e => e.CategoryId)
                .HasValue<Person>(0)
                .HasValue<Student>(1)
                .HasValue<Teacher>(2)
                .HasValue<TeachingAssistant>(3);

            b.HasKey(e => e.Id);

            b.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(e => e.IsOnRedList)
                .IsRequired();

            b.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(25);

            b.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(25);

            b.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(25);

            b.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId);
            b.HasOne(e => e.BuisnessField).WithMany().HasForeignKey(e => e.BuisnessFieldId);

            return builder;
        }

        public static ModelBuilder AddStudent(this ModelBuilder builder) 
        {
            var b = builder.Entity<Student>();

            b.Property(e => e.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(25);

            return builder;
        }

        public static ModelBuilder AddTeacher(this ModelBuilder builder) 
        {
            var b = builder.Entity<Teacher>();

            b.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(25);

            return builder;
        }

        public static ModelBuilder AddTeachingAssistant(this ModelBuilder builder)
        {
            var b = builder.Entity<TeachingAssistant>();

            b.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(25);

            return builder;
        }

        public static ModelBuilder AddCategories(this ModelBuilder builder)
        {
            var b = builder.Entity<Category>();

            b.HasKey(e => e.Id);

            b.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            return builder;
        }

        public static ModelBuilder AddBuisnessFields(this ModelBuilder builder)
        {
            var b = builder.Entity<BuisnessField>();

            b.HasKey(e => e.Id);

            b.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            return builder;
        }

        public static ModelBuilder BaseData(this ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new Category() { Id = 1, Name = "Etudiant" });
            builder.Entity<Category>().HasData(new Category() { Id = 2, Name = "Professeur" });
            builder.Entity<Category>().HasData(new Category() { Id = 3, Name = "Auxiliaire d’enseignement" });

            builder.Entity<BuisnessField>().HasData(new BuisnessField() { Id = 1, Name = "Informatique" });
            builder.Entity<BuisnessField>().HasData(new BuisnessField() { Id = 2, Name = "Sante" });

            return builder;
        }

        public static ModelBuilder MockData(this ModelBuilder builder)
        {
            builder.Entity<Student>().HasData(new Student() { Id = 1, EmailAddress = "AAA", FirstName = "AAA", LastName = "BBB", ReferenceNumber = "AAAA", IsOnRedList = false, BuisnessFieldId = 1, Password = "1234" });
            builder.Entity<Student>().HasData(new Student() { Id = 2, EmailAddress = "AA", FirstName = "CCC", LastName = "BBB", ReferenceNumber = "AAAA", IsOnRedList = true, BuisnessFieldId = 1, Password = "1234" });

            builder.Entity<Teacher>().HasData(new Teacher() { Id = 3, EmailAddress = "AAA", FirstName = "AAA", LastName = "BBB", PhoneNumber = "AAAA", IsOnRedList = false, BuisnessFieldId = 1, Password = "1234" });
            builder.Entity<Teacher>().HasData(new Teacher() { Id = 4, EmailAddress = "AA", FirstName = "CCC", LastName = "BBB", PhoneNumber = "AAAA", IsOnRedList = true, BuisnessFieldId = 1, Password = "1234" });

            builder.Entity<TeachingAssistant>().HasData(new TeachingAssistant() { Id = 5, EmailAddress = "AAA", FirstName = "AAA", LastName = "BBB", PhoneNumber = "AAAA", IsOnRedList = false, BuisnessFieldId = 1, Password = "1234" });
            builder.Entity<TeachingAssistant>().HasData(new TeachingAssistant() { Id = 6, EmailAddress = "AA", FirstName = "CCC", LastName = "BBB", PhoneNumber = "AAAA", IsOnRedList = true, BuisnessFieldId = 1, Password = "1234" });


            return builder;
        }
    }
}
