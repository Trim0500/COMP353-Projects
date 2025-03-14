using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Teamsession
    {
        public int TeamFormationIdFk { get; set; }
        public int SessionIdFk { get; set; }
        public int? Score { get; set; }

        public virtual Session SessionIdFkNavigation { get; set; } = null!;
        public virtual Teamformation TeamFormationIdFkNavigation { get; set; } = null!;
    }
}
