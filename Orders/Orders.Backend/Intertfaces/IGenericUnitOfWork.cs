using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Intertfaces
{
    public interface IGenericUnitOfWork<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<JsonResponseOjbect<T>> AddAsync(T model);

        Task<JsonResponseOjbect<T>> UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<T> GetAsync(int id);

        Task<Country> GetCountryAsync(int id);

        //Task<State> GetStateAsync(int id);
    }
}
