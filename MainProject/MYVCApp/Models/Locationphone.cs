using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Locationphone
    {
        public int LocationIdFk { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}
