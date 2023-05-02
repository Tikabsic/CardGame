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
    internal class PlayerCardConfig : IEntityTypeConfiguration<PlayerCard>
    {
        public void Configure(EntityTypeBuilder<PlayerCard> builder)
        {
            builder.HasKey(pc => new { pc.PlayerId, pc.CardId});
        }
    }
}
