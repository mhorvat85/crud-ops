﻿using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.Domain.Entities
{
  public class Person
  {
    [Key]
    public Guid PersonID { get; set; }

    [StringLength(40)]
    public string? PersonName { get; set; }

    [StringLength(40)]
    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(10)]
    public string? Gender { get; set; }

    public Guid? CountryID { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    public bool ReceiveNewsLetters { get; set; }

    public Country? Country { get; set; }
  }
}