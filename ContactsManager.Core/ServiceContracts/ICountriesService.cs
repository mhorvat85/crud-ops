using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
  public interface ICountriesService
  {
    Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);

    Task<List<CountryResponse>> GetAllCountries();

    Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);
  }
}