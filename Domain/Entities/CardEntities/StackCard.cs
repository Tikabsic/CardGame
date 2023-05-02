using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CardEntities
{
    public class StackCard
    {
        public Card Card { get; set; }
        public int CardId { get; set; }
        public Stack Stack { get; set; }
        public int StackId { get; set; }
    }
}
