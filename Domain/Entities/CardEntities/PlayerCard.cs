using Domain.Entities.PlayerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CardEntities
{
    public class PlayerCard
    {
        public Card Card { get; set; }
        public int CardId { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
    }
}
