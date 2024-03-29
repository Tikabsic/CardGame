﻿using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastruct.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByName(string Name)
        {
            var user = await _dbContext.Users.FirstAsync(u => u.Name == Name);

            return user;
        }

        public async Task<bool> IsUserNameTaken(string name)
        {
            var isUserNameTaken = await _dbContext.Users.AnyAsync(u => u.Name == name);

            return isUserNameTaken;
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
