﻿using ContactsManager.Core.Domain.Entities;

namespace ContactsManager.Core.DTO
{
  public class CountryAddRequest
  {
    public string? CountryName { get; set; }

    public Country ToCountry()
    {
      return new Country() { CountryName = CountryName };
    }
  }
}
