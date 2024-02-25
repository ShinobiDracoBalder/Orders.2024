using Azure;
using Orders.Shared.Responses;

namespace Orders.Backend.Services
{
    public interface IApiService
    {
        Task<JsonResponseOjbect<T>> GetAsync<T>(string servicePrefix, string controller);
    }
}
