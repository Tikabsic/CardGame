using Domain.Entities.CardEntities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IStackRepository
    {
        Task<Stack> GetStackAsync(string roomId);
    }
}
