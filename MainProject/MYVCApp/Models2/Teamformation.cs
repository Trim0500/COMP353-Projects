using System;
using System.Collections.Generic;

namespace MYVCApp.Models2;

public partial class Teamformation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CaptainIdFk { get; set; }

    public int? LocationIdFk { get; set; }

    public virtual Clubmember? CaptainIdFkNavigation { get; set; }

    public virtual Location? LocationIdFkNavigation { get; set; }

    public virtual ICollection<Teammember> Teammembers { get; set; } = new List<Teammember>();

    public virtual ICollection<Teamsession> Teamsessions { get; set; } = new List<Teamsession>();
}
