using ContactsManager.Core.Domain.Entities;

namespace ContactsManager.Core.Domain.RepositoryContracts
{
  public interface ICountriesRespository
  {
    Task<Country> AddCountry(Country country);

    Task<List<Country>> GetAllCountries();

    Task<Country?> GetCountryByCountryID(Guid? countryID);

    Task<Country?> GetCountryByCountryName(string countryName);
  }
}