using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Personnel
    {
        public Personnel()
        {
            Personnellocations = new HashSet<Personnellocation>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        [MaxLength(9)]
        public string SocialSecNum { get; set; } = null!;

        [MaxLength(12)]

        public string? MedCardNum { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(2)]
        [MinLength(2)]
        public string? Province { get; set; }

        [MaxLength(6)]
        [MinLength(6)]
        public string? PostalCode { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength (10)]
        public string? Mandate { get; set; }

        public virtual ICollection<Personnellocation> Personnellocations { get; set; }
    }
}
