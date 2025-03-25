using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q17Record
    {
        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("start date")]
        public DateTime? StartDate { get; set; }

        [Column("end date")]
        public DateTime? EndDate { get; set; }
    }
}