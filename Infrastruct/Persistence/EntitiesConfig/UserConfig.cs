using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruct.Persistence.EntitiesConfig
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(u => u.CreationDate)
                .HasDefaultValueSql("getutcdate()");

        }
    }
}
