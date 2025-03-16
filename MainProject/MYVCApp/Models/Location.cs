using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MYVCApp.Models
{
    public partial class Location
    {
        public Location()
        {
            Familymemberlocations = new HashSet<Familymemberlocation>();
            Locationphones = new HashSet<Locationphone>();
            Personnellocations = new HashSet<Personnellocation>();
            Sessions = new HashSet<Session>();
            Teamformations = new HashSet<Teamformation>();
        }

        public int Id { get; set; }

        [MaxLength(10)]
        public string? Type { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(6)]
        public string? PostalCode { get; set; }
        [MaxLength(2)]
        public string? Province { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
        [MaxLength(50)]
        public string? City { get; set; }
        [MaxLength(255)]
        public string? WebsiteUrl { get; set; }

        public int? Capacity { get; set; }

        public virtual ICollection<Familymemberlocation> Familymemberlocations { get; set; }
        public virtual ICollection<Locationphone> Locationphones { get; set; }
        public virtual ICollection<Personnellocation> Personnellocations { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Teamformation> Teamformations { get; set; }
    }
}
