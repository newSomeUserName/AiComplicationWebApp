using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Core.Models;

namespace UnifiedAIChat.Infrastructure.Persistence.Configuration
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasKey(m => m.ChatId);

            builder.Property(m => m.Content).IsRequired();

            builder.Property(m => m.Role).HasConversion<string>().HasMaxLength(20).IsRequired();

            builder.HasOne(m => m.Chat).WithMany(c => c.Messages).HasForeignKey(m => m.ChatId).OnDelete(DeleteBehavior.Cascade);

        }

    }
}
