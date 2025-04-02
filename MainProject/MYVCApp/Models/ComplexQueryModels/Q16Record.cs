using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q16Record
    {
        [Column("cmn")]
        public int Cmn { get; set; }

        [Column("first_name")]
        public string? FirstName {  get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("LocationNames")]
        public string? LocationNames { get; set; }
    }
}
