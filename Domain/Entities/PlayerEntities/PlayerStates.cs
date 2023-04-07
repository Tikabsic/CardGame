using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PlayerEntities
{
    public class PlayerStates
    {
        public bool IsCardDrewFromDeck { get; set; } = false;
        public bool IsCardThrownToStack { get; set; } = false;
        public bool IsPlayerAbleToThrowACard { get; set; }
    }
}
