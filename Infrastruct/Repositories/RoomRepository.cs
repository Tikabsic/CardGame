using Application.Interfaces.InfrastructureRepositories;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Infrastruct.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;

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
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SET IDENTITY_INSERT Rooms ON";

            await command.ExecuteNonQueryAsync();

            await _dbContext.Rooms.AddAsync(room);
            await _dbContext.SaveChangesAsync();

            command.CommandText = "SET IDENTITY_INSERT Rooms OFF";
            await command.ExecuteNonQueryAsync();

            return room;
        }

        public async Task<Room> RemoveRoomAsync(Room room)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SET IDENTITY_INSERT Rooms ON";

            await command.ExecuteNonQueryAsync();

            _dbContext.Rooms.Remove(room);

            await _dbContext.SaveChangesAsync();

            command.CommandText = "SET IDENTITY_INSERT Rooms OFF";
            await command.ExecuteNonQueryAsync();

            return room;
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            using var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SET IDENTITY_INSERT Rooms ON";

            await command.ExecuteNonQueryAsync();

            _dbContext.Rooms.Update(room);

            await _dbContext.SaveChangesAsync();

            command.CommandText = "SET IDENTITY_INSERT Rooms OFF";
            await command.ExecuteNonQueryAsync();

            return room;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            var rooms = await _dbContext.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<Room> GetRoomAsync(string roomId)
        {
            return await _dbContext.Rooms.FirstAsync(r => r.RoomId == roomId);
        }

    }
}
