using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Intertfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int id);

        Task<IEnumerable<T>> GetAsync();

        Task<JsonResponseOjbect<T>> AddAsync(T entity);

        Task<JsonResponseOjbect<T>> DeleteAsync(int id);

        Task<JsonResponseOjbect<T>> UpdateAsync(T entity);

        Task<Country> GetCountryAsync(int id);

        //Task<State> GetStateAsync(int id);
    }
}
