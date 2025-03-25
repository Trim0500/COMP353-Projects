using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q13Record
    {
        [Column("cmn")]
        public int? ClubMemberId { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("Age")]
        public int? Age { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("LocationsList")]
        public string? LocationsList { get; set; }
    }
}
