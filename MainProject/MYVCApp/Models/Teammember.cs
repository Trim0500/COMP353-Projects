using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Teammember
    {
        public int TeamFormationIdFk { get; set; }
        public int CmnFk { get; set; }

        [MaxLength(50)]
        public string? Role { get; set; }

        [Required]
        public DateTime? AssignmentDateTime { get; set; }

        public virtual Clubmember? CmnFkNavigation { get; set; } = null!;
        public virtual Teamformation? TeamFormationIdFkNavigation { get; set; } = null!;
    }
}
