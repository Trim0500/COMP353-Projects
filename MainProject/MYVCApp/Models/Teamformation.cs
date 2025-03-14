using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Teamformation
    {
        public Teamformation()
        {
            Teammembers = new HashSet<Teammember>();
            Teamsessions = new HashSet<Teamsession>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CaptainIdFk { get; set; }
        public int? LocationIdFk { get; set; }

        public virtual Clubmember? CaptainIdFkNavigation { get; set; }
        public virtual Location? LocationIdFkNavigation { get; set; }
        public virtual ICollection<Teammember> Teammembers { get; set; }
        public virtual ICollection<Teamsession> Teamsessions { get; set; }
    }
}
