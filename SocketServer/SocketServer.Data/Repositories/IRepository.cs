namespace SockerServer.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T element);
        T Remove(T element);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(object key);
        Task SaveChangesAsync();
        T Update(T element);
    }
}
