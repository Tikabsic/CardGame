﻿using Domain.Entities.RoomEntities;


namespace Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> CreateRoom();
    }
}