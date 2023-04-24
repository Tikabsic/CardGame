using Domain.Entities.PlayerEntities;
using Domain.Entities.RoomEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasMany(r => r.Players)
                .WithOne(p => p.GameRoom)
                .HasForeignKey(p => p.GameRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Chat)
                .WithOne(c => c.Room)
                .HasForeignKey(c => c.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
