﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Core.DTO
{
  public class RegisterDTO
  {
    [Required(ErrorMessage = "Name can't be blank")]
    public string? PersonName { get; set; }

    [Required(ErrorMessage = "Email can't be blank")]
    [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
    [Remote(action: "IsEmailAvailable", controller: "Account", ErrorMessage = "Email is already being used")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password can't be blank")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm Password can't be blank")]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Phone can't be blank")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Phone muber should contain numbers only")]
    [DataType(DataType.PhoneNumber)] 
    public string? Phone { get; set; }
  }
}
