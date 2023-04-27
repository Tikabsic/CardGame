using Domain.Entities.CardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {

            builder.HasMany(c => c.Decks)
                .WithMany(d => d.Cards);

            builder.HasMany(c => c.Stacks)
                .WithMany(s => s.Cards);
        }
    }
}
