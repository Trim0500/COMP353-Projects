using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q14Record
    {
        [Column("cmn")]
        public int? ClubMemberNumber { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("Age")]
        public int? Age { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}
