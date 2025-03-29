using System;
using System.Collections.Generic;

namespace MYVCApp.Models2;

public partial class Teammember
{
    public int TeamFormationIdFk { get; set; }

    public int CmnFk { get; set; }

    public string? Role { get; set; }

    public DateTime? AssignmentDateTime { get; set; }

    public virtual Clubmember CmnFkNavigation { get; set; } = null!;

    public virtual Teamformation TeamFormationIdFkNavigation { get; set; } = null!;
}
