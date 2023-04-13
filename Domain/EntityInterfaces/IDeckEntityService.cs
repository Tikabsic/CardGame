using Domain.Entities.CardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityInterfaces
{
    public interface IDeckEntityService
    {
        void GenerateDeck(Deck deck);
        void ShuffleDeck(Deck deck);
    }
}
