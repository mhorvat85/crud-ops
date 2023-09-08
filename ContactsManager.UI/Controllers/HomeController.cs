using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
  public class HomeController : Controller
  {
    [AllowAnonymous]
    [Route("Error")]
    public IActionResult Error()
    {
      IExceptionHandlerPathFeature? exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
      if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
      {
        if (exceptionHandlerPathFeature.Error.InnerException != null)
          ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.InnerException.Message;
        else
          ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
      }
      return View();
    }
  }
}
