using LogCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogCenter.Infra.Configuration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(512).IsRequired();
            builder.Property(x => x.Origin).HasMaxLength(128).IsRequired();
            
            builder.HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
