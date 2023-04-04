using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [NotMapped]
    public class GameRules
    {
        public int NumberOfRounds { get; set; }
        public int NumberOfPlayers { get; set; }
        public bool IsJokerAvailable { get; set; }

    }
}
