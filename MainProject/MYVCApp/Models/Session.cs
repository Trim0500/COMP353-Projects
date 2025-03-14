using System;
using System.Collections.Generic;

namespace MYVCApp.Models
{
    public partial class Session
    {
        public Session()
        {
            Teamsessions = new HashSet<Teamsession>();
        }

        public int Id { get; set; }
        public string? EventType { get; set; }
        public DateTime? EventDateTime { get; set; }
        public int? LocationIdFk { get; set; }

        public virtual Location? LocationIdFkNavigation { get; set; }
        public virtual ICollection<Teamsession> Teamsessions { get; set; }
    }
}
