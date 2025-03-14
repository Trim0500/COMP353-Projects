using System;
using System.Collections.Generic;

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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string SocialSecNum { get; set; } = null!;
        public string? MedCardNum { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PostalCode { get; set; }

        public virtual ICollection<Clubmember> Clubmembers { get; set; }
        public virtual ICollection<Familymemberlocation> Familymemberlocations { get; set; }
        public virtual ICollection<Secondaryfamilymember> Secondaryfamilymembers { get; set; }
    }
}
