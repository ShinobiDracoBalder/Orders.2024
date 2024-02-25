using Orders.Backend.Services;

namespace Orders.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;

        public SeedDb(DataContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
        private async Task CheckCountriesAsync()
        {
            //if (!_context.Countries.Any())
            //{
            //    var responseCountries = await _apiService.GetAsync<List<CountryResponse>>("/v1", "/countries");
            //    if (responseCountries.ItsSuccessful && responseCountries.code == 200)
            //    {
            //        var countries = responseCountries.ResultModel!;
            //        foreach (var countryResponse in countries)
            //        {
            //            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
            //            if (country == null)
            //            {
            //                country = new() { Name = countryResponse.Name!, States = new List<State>() };
            //                var responseStates = await _apiService.GetAsync<List<StateResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states");
            //                if (responseStates.WasSuccess)
            //                {
            //                    var states = responseStates.Result!;
            //                    foreach (var stateResponse in states!)
            //                    {
            //                        var state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
            //                        if (state == null)
            //                        {
            //                            state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
            //                            var responseCities = await _apiService.GetAsync<List<CityResponse>>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
            //                            if (responseCities.WasSuccess)
            //                            {
            //                                var cities = responseCities.Result!;
            //                                foreach (var cityResponse in cities)
            //                                {
            //                                    if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
            //                                    {
            //                                        continue;
            //                                    }
            //                                    var city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
            //                                    if (city == null)
            //                                    {
            //                                        state.Cities.Add(new City() { Name = cityResponse.Name! });
            //                                    }
            //                                }
            //                            }
            //                            if (state.CitiesNumber > 0)
            //                            {
            //                                country.States.Add(state);
            //                            }
            //                        }
            //                    }
            //                }
            //                if (country.StatesNumber > 0)
            //                {
            //                    _context.Countries.Add(country);
            //                    await _context.SaveChangesAsync();
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
