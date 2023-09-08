using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.DTO;
using ContactsManager.UI.Filters.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
  [Route("[controller]/[action]")]
  public class AccountController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
    }

    [HttpGet]
    [Authorize("NotAuthenticated")]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    [Authorize("NotAuthenticated")]
    [TypeFilter(typeof(ModelStateValidationActionFilter))]
    public async Task<IActionResult> Register(RegisterDTO signDTO)
    {
      ApplicationUser user = new() { UserName = signDTO.Email, Email = signDTO.Email, PhoneNumber = signDTO.Phone, PersonName = signDTO.PersonName };

      IdentityResult result = await _userManager.CreateAsync(user, signDTO.Password!);

      if (result.Succeeded)
      {
        if (await _roleManager.FindByNameAsync("User") is null)
        {
          ApplicationRole role = new() { Name = "User" };
          await _roleManager.CreateAsync(role);
        }
        await _userManager.AddToRoleAsync(user, "User");

        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction(nameof(PersonsController.Index), "Persons");
      }
      else
      {
        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError("Register", error.Description);
        }

        return View(signDTO);
      }
    }

    [HttpGet]
    [Authorize("NotAuthenticated")]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    [Authorize("NotAuthenticated")]
    [TypeFilter(typeof(ModelStateValidationActionFilter))]
    public async Task<IActionResult> Login(LoginDTO signDTO)
    {
      await CreateAdmin();

      var result = await _signInManager.PasswordSignInAsync(signDTO.Email!, signDTO.Password!, isPersistent: false, lockoutOnFailure: false);

      if (result.Succeeded)
      {
        ApplicationUser? user = await _userManager.FindByEmailAsync(signDTO.Email!);
        if (user != null)
        {
          if (await _userManager.IsInRoleAsync(user, "Admin"))
          {
            return RedirectToAction("Index", "Home", new { area = "Admin" });
          }
        }
        return RedirectToAction(nameof(PersonsController.Index), "Persons");
      }

      ModelState.AddModelError("Login", "Invalid email or password");
      return View(signDTO);
    }

    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction(nameof(PersonsController.Index), "Persons");
    }

    [Authorize("NotAuthenticated")]
    public async Task<IActionResult> IsEmailAvailable(string email)
    {
      ApplicationUser? user = await _userManager.FindByEmailAsync(email);
      if (user == null)
      {
        return Json(true);
      }
      else
      {
        return Json(false);
      }
    }

    public async Task CreateAdmin()
    {
      if (!await _roleManager.RoleExistsAsync("Admin"))
      {
        ApplicationRole role = new() { Name = "Admin" };
        await _roleManager.CreateAsync(role);
      }

      if (await _userManager.FindByEmailAsync("admin@example.com") == null)
      {
        ApplicationUser user = new() { UserName = "admin@example.com", Email = "admin@example.com" };
        IdentityResult result = await _userManager.CreateAsync(user, "admin");
        if (result.Succeeded)
        {
          await _userManager.AddToRoleAsync(user, "Admin");
        }
      }
    }
  }
}
