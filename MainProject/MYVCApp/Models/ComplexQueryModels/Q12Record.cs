using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q12Record
    {
        [Column("MembershipNumber")]
        public int? MembershipNumber { get; set; }

        [Column("MemberFirstName")]
        public string? MemberFirstName { get; set; }

        [Column("MemberLastName")]
        public string? MemberLastName { get; set; }

        [Column("Age")]
        public int? Age { get; set; }

        [Column("JoinDate")]
        public DateTime? JoinDate { get; set; }

        [Column("MemberPhone")]
        public string? MemberPhone { get; set; }

        [Column("MemberEmail")]
        public string? MemberEmail { get; set; }

        [Column("LocationNames")]
        public string? LocationNames { get; set; }
    }
}
