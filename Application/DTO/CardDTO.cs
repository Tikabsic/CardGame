using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
    }
}
