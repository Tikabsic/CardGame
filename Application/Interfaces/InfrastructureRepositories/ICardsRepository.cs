using Domain.Entities.CardEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface ICardsRepository
    {
        Task<List<Card>> GetCardsAsync();
    }
}
