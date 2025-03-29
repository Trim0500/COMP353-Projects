using System;
using System.Collections.Generic;

namespace MYVCApp.Models;

public partial class Secondaryfamilymember
{
    public int PrimaryFamilyMemberIdFk { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? RelationshipToPrimary { get; set; }

    public virtual Familymember? PrimaryFamilyMemberIdFkNavigation { get; set; } = null!;
}
