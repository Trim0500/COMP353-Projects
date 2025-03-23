using System;
using System.Collections.Generic;

namespace MYVCApp.Temp
{
    public partial class Familymember
    {
        public Familymember()
        {
            Clubmembers = new HashSet<Clubmember>();
            Familymemberlocations = new HashSet<Familymemberlocation>();
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

        public virtual Secondaryfamilymember? Secondaryfamilymember { get; set; }
        public virtual ICollection<Clubmember> Clubmembers { get; set; }
        public virtual ICollection<Familymemberlocation> Familymemberlocations { get; set; }
    }
}
