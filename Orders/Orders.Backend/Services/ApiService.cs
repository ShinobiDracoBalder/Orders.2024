using Azure;
using Orders.Shared.Responses;
using System.Net;
using System.Text.Json;

namespace Orders.Backend.Services
{
    public class ApiService : IApiService
    {
        private readonly string _urlBase;
        private readonly string _tokenName;
        private readonly string _tokenValue;
        public ApiService(IConfiguration configuration)
        {
            _urlBase = configuration["CoutriesAPI:urlBase"]!;
            _tokenName = configuration["CoutriesAPI:tokenName"]!;
            _tokenValue = configuration["CoutriesAPI:tokenValue"]!;
        }
        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        public async Task<JsonResponseOjbect<T>> GetAsync<T>(string servicePrefix, string controller)
        {
            try
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(_urlBase),
                };

                client.DefaultRequestHeaders.Add(_tokenName, _tokenValue);
                var url = $"{servicePrefix}{controller}";
                var responseHttp = await client.GetAsync(url);
                var response = await responseHttp.Content.ReadAsStringAsync();
                if (!responseHttp.IsSuccessStatusCode)
                {
                    return new JsonResponseOjbect<T>
                    {
                        code = (int)HttpStatusCode.BadRequest,
                        ItsSuccessful = false,
                        Message = response
                    };
                }

                return new JsonResponseOjbect<T>
                {
                    code = (int)HttpStatusCode.OK,
                    ItsSuccessful = true,
                    ResultModel = JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!
                };
            }
            catch (Exception ex)
            {
                return new JsonResponseOjbect<T>
                {
                    code = (int)HttpStatusCode.BadRequest,
                    ItsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
