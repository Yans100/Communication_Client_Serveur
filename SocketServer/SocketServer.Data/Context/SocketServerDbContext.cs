using Microsoft.EntityFrameworkCore;
using SockerServer.Data.Models;

namespace SockerServer.Data.Context
{
    public class SocketServerDbContext(DbContextOptions<SocketServerDbContext> options) : DbContext(options)
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BuisnessField> BuisnessFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddPerson();
            modelBuilder.AddTeacher();
            modelBuilder.AddTeachingAssistant();
            modelBuilder.AddStudent();
            modelBuilder.AddCategories();
            modelBuilder.AddBuisnessFields();

            modelBuilder.BaseData();

            modelBuilder.MockData();
        }
    }
}
