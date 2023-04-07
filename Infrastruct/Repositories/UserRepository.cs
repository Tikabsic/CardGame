using Application.DTO;
using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastruct.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByName(string Name)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == Name);

            return user;
        }
    }
}
