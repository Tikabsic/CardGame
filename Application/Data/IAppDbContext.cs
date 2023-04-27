using Domain.Entities;
using Domain.Entities.CardEntities;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore;



namespace Application.Data
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Card> Cards { get; set; }
    }
}
