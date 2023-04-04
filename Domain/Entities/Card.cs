﻿using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [NotMapped]
    public class Card
    {
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
    }
}
