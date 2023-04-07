using Domain.Entities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IAccountRepository
    {
        Task AddUser(User user);
    }
}

