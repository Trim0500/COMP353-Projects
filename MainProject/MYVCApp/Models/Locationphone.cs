using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Locationphone
    {
        public int LocationIdFk { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string PhoneNumber { get; set; } = null!;
    }
}
