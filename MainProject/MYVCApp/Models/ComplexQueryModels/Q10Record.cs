using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q10Record
    {
        [Column("MembershipNumber")]
        public int? MembershipNumber { get; set; }

        [Column("MemberFirstName")]
        public string? MemberFirstName { get; set; }

        [Column("MemberLastName")]
        public string? MemberLastName { get; set; }
    }
}
