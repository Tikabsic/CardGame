using Domain.Entities.CardEntities;

namespace Domain.Interfaces
{
    public interface IDeckEntityService
    {
        void ShuffleDeck(Deck deck);
    }
}
