using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Personnel
    {
        public Personnel()
        {
            Personnellocations = new HashSet<Personnellocation>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string SocialSecNum { get; set; } = null!;
        public string? MedCardNum { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? Email { get; set; }
        public string? Mandate { get; set; }

        public virtual ICollection<Personnellocation> Personnellocations { get; set; }
    }
}
