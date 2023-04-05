using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task AddUser(User user);
    }
}

