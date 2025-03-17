using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Secondaryfamilymember
    {
        public int PrimaryFamilyMemberIdFk { get; set; }
        
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [MaxLength(10)]
        [MinLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(20)]
        public string? RelationshipToPrimary { get; set; }

        public virtual Familymember? PrimaryFamilyMemberIdFkNavigation { get; set; } = null!;
    }
}
