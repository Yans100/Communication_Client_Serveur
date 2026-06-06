using Microsoft.EntityFrameworkCore;
using SockerServer.Data.Context;

namespace SockerServer.Data.Repositories
{
    internal class Repository<T>(SocketServerDbContext context) : IRepository<T> where T : class
    {
        private readonly SocketServerDbContext context = context;

        public async Task<T> AddAsync(T element)
        {
            var res = await context.AddAsync(element);
            return res.Entity;
        }

        public T Remove(T element)
        {
            var res = context.Remove(element);
            return res.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(object key)
        {
            var elem = await context.Set<T>().FindAsync(key);

            if (elem is not null)
                context.Entry(elem).State = EntityState.Detached;

            return elem;
        }

        public T Update(T element)
        {
            return context.Update(element).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
