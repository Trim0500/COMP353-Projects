using System;
using System.Collections.Generic;

namespace MYVCApp.Models2;

public partial class Location
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public string? PostalCode { get; set; }

    public string? Province { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? WebsiteUrl { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Familymemberlocation> Familymemberlocations { get; set; } = new List<Familymemberlocation>();

    public virtual ICollection<Locationphone> Locationphones { get; set; } = new List<Locationphone>();

    public virtual ICollection<Personnellocation> Personnellocations { get; set; } = new List<Personnellocation>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Teamformation> Teamformations { get; set; } = new List<Teamformation>();
}
