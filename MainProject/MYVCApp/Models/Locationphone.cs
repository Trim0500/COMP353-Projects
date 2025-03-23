using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models
{
    public partial class Locationphone
    {
        public int LocationIdFk { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public virtual Location? LocationIdFkNavigation { get; set; }
    }
}
