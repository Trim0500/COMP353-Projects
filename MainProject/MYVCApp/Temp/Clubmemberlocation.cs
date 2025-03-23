using System;
using System.Collections.Generic;

namespace MYVCApp.Temp
{
    public partial class Clubmemberlocation
    {
        public int LocationIdFk { get; set; }
        public int CmnFk { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Clubmember CmnFkNavigation { get; set; } = null!;
        public virtual Location LocationIdFkNavigation { get; set; } = null!;
    }
}
