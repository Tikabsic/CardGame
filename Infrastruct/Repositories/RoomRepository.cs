using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.RoomEntities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Repositories
{
    internal class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public RoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Room> SaveRoomAsync(Room room)
        {
            await _dbContext.Rooms.AddAsync(room);

            await _dbContext.SaveChangesAsync();

            return room;
        }

        public async Task<Room> RemoveRoomAsync(Room room)
        {
            _dbContext.Rooms.Remove(room);

            await _dbContext.SaveChangesAsync();

            return room;
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {

            _dbContext.Rooms.Update(room);

            await _dbContext.SaveChangesAsync();

            return room;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            var rooms = await _dbContext.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            var room = await _dbContext.Rooms.Include(r => r.Deck).FirstAsync(r => r.RoomId == roomId);
            return room;
        }

    }
}
