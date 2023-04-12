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

        public async Task<User> GetUserByName(string Name)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == Name);

            return user;
        }

        public async Task<bool> IsUserNameTaken(string name)
        {
            var isUserNameTaken = await _dbContext.Users.AnyAsync(u => u.Name == name);

            return isUserNameTaken;
        }

    }
}
