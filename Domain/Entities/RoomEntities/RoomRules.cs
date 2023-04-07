using Domain.Entities.PlayerEntities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.RoomEntities
{
    [NotMapped]
    public class RoomRules
    {

        public Numbers NumberOfRounds { get; set; }
        public Numbers NumberOfPlayers { get; set; }

        public void SetNumberOfRounds(Numbers number)
        {
            NumberOfRounds = number;
        }

        public void SetNumberOfPlayers(Numbers number)
        {
            NumberOfPlayers = number;
        }
    }
}
