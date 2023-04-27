using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.CardEntities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Repositories
{
    internal class StackRepository : IStackRepository
    {
        private readonly AppDbContext _dbContext;

        public StackRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Stack> GetStackAsync(string roomId)
        {
            return await _dbContext.Stacks.FirstOrDefaultAsync(s => s.RoomId == roomId);
        }
    }
}
