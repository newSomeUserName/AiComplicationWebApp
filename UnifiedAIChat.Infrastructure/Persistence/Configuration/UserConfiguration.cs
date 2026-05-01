using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Infrastructure.Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u=> u.Id);

            builder.Property(u=> u.Email).HasMaxLength(256).IsRequired();

            builder.HasIndex(u=>u.Email).IsUnique();

            builder.Property(u=> u.PasswordHash).HasMaxLength(512).IsRequired();

            builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(20).IsRequired();

            builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        }
    }
}
