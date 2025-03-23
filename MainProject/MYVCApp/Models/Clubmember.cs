using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Clubmember
    {
        public Clubmember()
        {
            Payments = new HashSet<Payment>();
            Teamformations = new HashSet<Teamformation>();
            Teammembers = new HashSet<Teammember>();
        }

        public int Cmn { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Height { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Weight { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        public string SocialSecNum { get; set; } = null!;

        [MaxLength(12)]
        public string? MedCardNum { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MinLength(2)]
        [MaxLength(2)]
        public string? Province { get; set; }

        [MaxLength(6)]
        [MinLength(6)]
        public string? PostalCode { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public string? ProgressReport { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public int? FamilyMemberIdFk { get; set; }

        [MaxLength(20)]
        public string? PrimaryRelationship { get; set; }
        
        [MaxLength(20)]
        public string? SecondaryRelationship { get; set; }

        public virtual Familymember? FamilyMemberIdFkNavigation { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Teamformation> Teamformations { get; set; }
        public virtual ICollection<Teammember> Teammembers { get; set; }
    }
}
