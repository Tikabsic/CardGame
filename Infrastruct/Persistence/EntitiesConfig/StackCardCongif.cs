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
    internal class StackCardCongif : IEntityTypeConfiguration<StackCard>
    {
        public void Configure(EntityTypeBuilder<StackCard> builder)
        {
            builder.HasKey(sc => new { sc.StackId, sc.CardId });
        }
    }
}
