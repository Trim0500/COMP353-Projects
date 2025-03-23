using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Familymemberlocation
    {
        public int LocationIdFk { get; set; }
        public int FamilyMemberIdFk { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Familymember? FamilyMemberIdFkNavigation { get; set; } = null!;
        public virtual Location? LocationIdFkNavigation { get; set; } = null!;
    }
}
