using Domain.Entities;

namespace Application.Interfaces.InfrastructureRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByName(string Name);
        Task<bool> IsUserNameTaken(string name);
    }
}
