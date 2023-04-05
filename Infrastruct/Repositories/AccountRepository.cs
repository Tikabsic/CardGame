using Domain.Entities;
using Application.Interfaces.Repositories;
using Infrastruct.Persistence;


namespace Infrastruct.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            await _dbContext.User.AddAsync(user);

            await _dbContext.SaveChangesAsync();

        }
    }
}
