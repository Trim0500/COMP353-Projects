using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q7Record
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("province")]
        public string? Province { get; set; }

        [Column("postal code")]
        public string? PostalCode { get; set; }

        [Column("website")]
        public string? Website { get; set; }

        [Column("number of active members")]
        public int? NumberOfActiveMembers { get; set; }

        [Column("general manager first name")]
        public string? GeneralManagerFirstName { get;set; }

        [Column("general manager last name")]
        public string? GeneralManagerLastName { get; set; }
    }
}
