using Domain.Entities.CardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasMany(c => c.DeckCards)
                .WithOne(dc => dc.Card)
                .HasForeignKey(c => c.CardId);

            builder.HasMany(c => c.StackCards)
                .WithOne(sc => sc.Card)
                .HasForeignKey(sc => sc.CardId);

            builder.HasMany(c => c.PlayerCards)
                .WithOne(pc => pc.Card)
                .HasForeignKey(pc => pc.CardId);
        }
    }
}
