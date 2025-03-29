using System;
using System.Collections.Generic;

namespace MYVCApp.Models2;

public partial class Clubmember
{
    public int Cmn { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Email { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string SocialSecNum { get; set; } = null!;

    public string? MedCardNum { get; set; }

    public string? PhoneNumber { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public string? PostalCode { get; set; }

    public string? Address { get; set; }

    public string? ProgressReport { get; set; }

    public bool IsActive { get; set; }

    public int? FamilyMemberIdFk { get; set; }

    public string PrimaryRelationship { get; set; } = null!;

    public string? SecondaryRelationship { get; set; }

    public string Gender { get; set; } = null!;

    public virtual Familymember? FamilyMemberIdFkNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Teamformation> Teamformations { get; set; } = new List<Teamformation>();

    public virtual ICollection<Teammember> Teammembers { get; set; } = new List<Teammember>();
}
