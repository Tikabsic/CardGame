using Domain.Entities.CardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class DeckConfig : IEntityTypeConfiguration<Deck>
    {
        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.HasOne(d => d.Room)
                .WithOne(r => r.Deck)
                .HasForeignKey<Deck>(d => d.RoomId);

            builder.HasMany(d => d.Cards)
                .WithOne(dc => dc.Deck)
                .HasForeignKey(dc => dc.DeckId);
        }
    }
}
