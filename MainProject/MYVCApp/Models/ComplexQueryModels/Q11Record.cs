using System.ComponentModel.DataAnnotations.Schema;

namespace MYVCApp.Models.ComplexQueryModels
{
    public class Q11Record
    {
        [Column("LocationName")]
        public string? LocationName { get; set; }

        [Column("TrainingSessions")]
        public int? TrainingSessions { get; set; }

        [Column("TotalTrainingSessionPlayers")]
        public int? TotalTrainingSessionPlayers { get; set; }

        [Column("GameSessions")]
        public int? GameSessions { get; set; }

        [Column("TotalGameSessionPlayers")]
        public int? TotalGameSessions { get; set; }
    }
}
