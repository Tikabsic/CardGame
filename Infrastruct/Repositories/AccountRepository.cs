using Application.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastruct.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
