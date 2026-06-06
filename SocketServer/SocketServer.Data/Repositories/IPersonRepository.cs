using SockerServer.Data.Models;

namespace SockerServer.Data.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<T>> GetPeopleOfTypeAsync<T>();
        Task<Person?> GetWithDetailsAsync(int id);
        Task<IEnumerable<T>> GetPeopleOfTypeInBuisnesField<T>(Enums.BuisnessField field);
        Task<IEnumerable<Person>> GetPeopleByFirstNameAsync(string name);
    }
}
