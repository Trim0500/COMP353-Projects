using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Personnellocation
    {
        public int PersonnelIdFk { get; set; }
        public int LocationIdFk { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Role { get; set; }

        public virtual Location? LocationIdFkNavigation { get; set; }
        public virtual Personnel? PersonnelIdFkNavigation { get; set; }
    }
}
