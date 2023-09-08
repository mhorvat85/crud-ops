using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.Domain.Entities
{
  public class Country
  {
    [Key]
    public Guid CountryID { get; set; }

    [StringLength(40)]
    public string? CountryName { get; set; }
  }
}