using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q9Record
    {
        [Column("head coach first name")]
        public string? HeadCoachFirstName { get; set; }

        [Column("head coach last name")]
        public string? HeadCoachLastName { get; set; }

        [Column("event_date_time")]
        public DateTime? EventDateTime { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("event_type")]
        public string? EventType { get; set; }

        [Column("team name")]
        public string? TeamName { get; set; }

        [Column("score")]
        public int? Score { get; set; }

        [Column("player first name")]
        public string? PlayerFirstName { get; set; }

        [Column("player last name")]
        public string? PlayerLastName { get; set; }

        [Column("role")]
        public string? Role { get; set; }
    }
}
