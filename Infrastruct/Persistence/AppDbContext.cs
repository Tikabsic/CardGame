using Application.Data;
using Domain.Entities;
using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastruct.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Player> Players { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}
