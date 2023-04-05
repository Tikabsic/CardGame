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
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
