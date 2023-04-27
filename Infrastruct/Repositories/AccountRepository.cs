using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities;
using Infrastruct.Persistence;

namespace Infrastruct.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }
    }
}
