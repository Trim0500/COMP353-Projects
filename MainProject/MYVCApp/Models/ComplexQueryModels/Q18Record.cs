using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q18Record
    {
        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("DATE_ADD(dob, INTERVAL 18 YEAR)")]
        public DateTime? DeactivationDate { get; set; }

        [Column("last location name")]
        public string? LastLocationName { get; set; }

        [Column("latest role")]
        public string? LatestRole { get; set; }
    }
}
