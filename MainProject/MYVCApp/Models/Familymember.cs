using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Familymember
    {
        public Familymember()
        {
            Clubmembers = new HashSet<Clubmember>();
            Familymemberlocations = new HashSet<Familymemberlocation>();
            Secondaryfamilymembers = new HashSet<Secondaryfamilymember>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        public string SocialSecNum { get; set; } = null!;

        [MaxLength(12)]
        public string? MedCardNum { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MinLength(2)]
        [MaxLength(2)]
        public string? Province { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [MinLength(6)]
        [MaxLength(6)]
        public string? PostalCode { get; set; }


        public virtual ICollection<Clubmember> Clubmembers { get; set; }
        public virtual ICollection<Familymemberlocation> Familymemberlocations { get; set; }
        public virtual ICollection<Secondaryfamilymember> Secondaryfamilymembers { get; set; }
    }
}
