﻿using Domain.Entities.RoomEntities;


namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> CreateRoom();
        Task SetGameAdminAsync(string roomId);

        Task<Room> GetRoomInfo(string roomId);
        Task<Room> JoinRoomByIdAsync(string roomId);
    }
}
