using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CardEntities
{
    public class Card
    {
        [NotMapped]
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
    }
}
