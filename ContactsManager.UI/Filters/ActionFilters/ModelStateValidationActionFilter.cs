using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactsManager.UI.Filters.ActionFilters
{
  public class ModelStateValidationActionFilter : IAsyncActionFilter
  {
    private readonly ICountriesService _countriesService;

    public ModelStateValidationActionFilter(ICountriesService countriesService)
    {
      _countriesService = countriesService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      if (context.Controller is PersonsController personsController)
      {
        if (!personsController.ModelState.IsValid)
        {
          List<CountryResponse> countries = await _countriesService.GetAllCountries();
          personsController.ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

          personsController.ViewBag.Errors = personsController.ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage).ToList();

          var personRequest = context.ActionArguments["personRequest"];

          context.Result = personsController.View(personRequest);
        }
        else
        {
          await next();
        }
      }
      else if (context.Controller is AccountController accountController)
      {
        if (!accountController.ModelState.IsValid)
        {
          accountController.ViewBag.Errors = accountController.ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage).ToList();

          var signDTO = context.ActionArguments["signDTO"];

          context.Result = accountController.View(signDTO);
        }
        else
        {
          await next();
        }
      }
      else
      {
        await next();
      }
    }
  }
}
