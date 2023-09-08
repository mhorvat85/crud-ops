﻿using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ContactsManager.Infrastructure.DatabaseContext
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Country>().ToTable("Countries");
      modelBuilder.Entity<Person>().ToTable("Persons");

      string countriesJson = File.ReadAllText("countries.json");
      List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(countriesJson);

      foreach (Country country in countries!)
        modelBuilder.Entity<Country>().HasData(country);

      string personsJson = File.ReadAllText("persons.json");
      List<Person>? persons = JsonSerializer.Deserialize<List<Person>>(personsJson);

      foreach (Person person in persons!)
        modelBuilder.Entity<Person>().HasData(person);
    }
  }
}
