using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Services;
using ContactsManager.Infrastructure.DatabaseContext;
using ContactsManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ContactsManager.UI.StartupExtensions
{
  public static class ConfigureServicesExtension
  {
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddControllersWithViews();

      services.AddScoped<ICountriesService, CountriesService>();
      services.AddScoped<IPersonsService, PersonsService>();
      services.AddScoped<ICountriesRespository, CountriesRepository>();
      services.AddScoped<IPersonsRepository, PersonsRepository>();

      services.AddDbContext<ApplicationDbContext>(optionsAction =>
      {
        optionsAction.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddIdentity<ApplicationUser, ApplicationRole>(setupAction =>
      {
        setupAction.Password.RequiredLength = 5;
        setupAction.Password.RequireNonAlphanumeric = false;
        setupAction.Password.RequireUppercase = false;
        setupAction.Password.RequireLowercase = true;
        setupAction.Password.RequireDigit = false;
        setupAction.Password.RequiredUniqueChars = 3;
      })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
        .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

      services.AddAuthorization(configure =>
      {
        configure.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        configure.AddPolicy("NotAuthenticated", configurePolicy =>
        {
          configurePolicy.RequireAssertion(handler =>
          {
            return !handler.User.Identity!.IsAuthenticated;
          });
        });
      });

      services.ConfigureApplicationCookie(configure =>
      {
        configure.AccessDeniedPath = "/Persons/Index";
      });

      return services;
    }
  }
}
