using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CardEntities
{
    public class DeckCard
    {
        public Card Card { get; set; }
        public int CardId { get; set; }

        public Deck Deck { get; set; }
        public int DeckId { get; set; }
    }
}
