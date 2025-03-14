using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Location
    {
        public Location()
        {
            Familymemberlocations = new HashSet<Familymemberlocation>();
            Personnellocations = new HashSet<Personnellocation>();
            Sessions = new HashSet<Session>();
            Teamformations = new HashSet<Teamformation>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? PostalCode { get; set; }
        public string? Province { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? WebsiteUrl { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<Familymemberlocation> Familymemberlocations { get; set; }
        public virtual ICollection<Personnellocation> Personnellocations { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Teamformation> Teamformations { get; set; }
    }
}
