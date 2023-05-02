using Domain.Entities.PlayerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(p => p.GameRoomId)
                .IsRequired(false);

            builder.HasMany(p => p.Hand)
                .WithOne(pc => pc.Player)
                .HasForeignKey(pc => pc.PlayerId);

        }
    }
}
