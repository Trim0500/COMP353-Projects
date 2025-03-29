using MYVCApp.Models;
using System;
using System.Collections.Generic;

namespace MYVCApp.Models2;

public partial class Familymember
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Email { get; set; }

    public string SocialSecNum { get; set; } = null!;

    public string? MedCardNum { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PostalCode { get; set; }

    public virtual ICollection<Clubmember>? Clubmembers { get; set; } = new List<Clubmember>();

    public virtual ICollection<Familymemberlocation>? Familymemberlocations { get; set; } = new List<Familymemberlocation>();

    public virtual Secondaryfamilymember? Secondaryfamilymember { get; set; }
}
