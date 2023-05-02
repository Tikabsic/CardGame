using Domain.Entities.CardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class DeckCardConfig : IEntityTypeConfiguration<DeckCard>
    {
        public void Configure(EntityTypeBuilder<DeckCard> builder)
        {
            builder.HasKey(cd => new { cd.CardId, cd.DeckId });
        }
    }
}
