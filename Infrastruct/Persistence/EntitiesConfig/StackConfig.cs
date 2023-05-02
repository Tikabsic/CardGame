using Domain.Entities.CardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class StackConfig : IEntityTypeConfiguration<Stack>
    {
        public void Configure(EntityTypeBuilder<Stack> builder)
        {
            builder.HasOne(s => s.Room)
                .WithOne(r => r.Stack)
                .HasForeignKey<Stack>(s => s.RoomId);

            builder.HasMany(s => s.Cards)
                .WithOne(sc => sc.Stack)
                .HasForeignKey(sc => sc.StackId);
        }
    }
}
