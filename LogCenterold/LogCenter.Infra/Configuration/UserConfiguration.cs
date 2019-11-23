using LogCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogCenter.Infra.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Token).HasMaxLength(256).IsRequired();
        }
    }
}
