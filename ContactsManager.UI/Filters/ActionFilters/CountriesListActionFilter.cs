using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactsManager.UI.Filters.ActionFilters
{
  public class CountriesListActionFilter : IAsyncActionFilter
  {
    private readonly ICountriesService _countriesService;

    public CountriesListActionFilter(ICountriesService countriesService)
    {
      _countriesService = countriesService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      await next();

      PersonsController personsController = (PersonsController)context.Controller;

      List<CountryResponse> countries = await _countriesService.GetAllCountries();
      personsController.ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() }).OrderBy(temp => temp.Text);
    }
  }
}
