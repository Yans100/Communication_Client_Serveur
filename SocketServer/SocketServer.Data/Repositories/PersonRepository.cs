using Microsoft.EntityFrameworkCore;
using SockerServer.Data.Context;
using SockerServer.Data.Models;

namespace SockerServer.Data.Repositories
{
    internal class PersonRepository(SocketServerDbContext context) : Repository<Person>(context), IPersonRepository
    {
        private readonly SocketServerDbContext context = context;

        public async Task<IEnumerable<T>> GetPeopleOfTypeAsync<T>()
        {
            return await context.People.AsNoTracking()
                .Include(p => p.Category).AsNoTracking()
                .Include(p => p.BuisnessField).AsNoTracking()
                .OfType<T>()              
                .ToListAsync();
        } 

        public async Task<Person?> GetWithDetailsAsync(int id)
        {
            return await context.People.AsNoTracking()
                .Where(p => p.Id == id)
                .Include(p => p.Category).AsNoTracking()
                .Include(p => p.BuisnessField).AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetPeopleOfTypeInBuisnesField<T>(Enums.BuisnessField field)
        {
            return await context.People.AsNoTracking()
                .Where(p => p.BuisnessFieldId == (int)field)
                .Include(p => p.Category).AsNoTracking()
                .Include(p => p.BuisnessField).AsNoTracking()
                .OfType<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetPeopleByFirstNameAsync(string name)
        {
            return await context.People.AsNoTracking()
                .Where(p => p.FirstName == name)
                .ToListAsync();
        }
    }
}
