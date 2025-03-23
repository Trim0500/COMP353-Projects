using System;
using System.Collections.Generic;

namespace MYVCApp.Temp
{
    public partial class Locationphone
    {
        public int LocationIdFk { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public virtual Location LocationIdFkNavigation { get; set; } = null!;
    }
}
