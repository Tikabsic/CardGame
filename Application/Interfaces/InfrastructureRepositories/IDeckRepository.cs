using Domain.Entities.CardEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IDeckRepository
    {
        Task UpdateDeckAsync(int deckId);
        Task GenerateDeckAsync(int deckId);
        Task<Deck> GetDeckAsync(string roomId);
    }
}
