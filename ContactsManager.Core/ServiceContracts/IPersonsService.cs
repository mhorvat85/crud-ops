using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;

namespace ContactsManager.Core.ServiceContracts
{
  public interface IPersonsService
  {
    Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest);

    Task<List<PersonResponse>> GetAllPersons();

    Task<PersonResponse?> GetPersonByPersonID(Guid? personID);

    Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString);

    List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

    Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest);

    Task<bool> DeletePerson(Guid? personID);
  }
}
