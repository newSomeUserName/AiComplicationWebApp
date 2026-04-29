using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Core.Models;

namespace UnifiedAIChat.Infrastructure.Persistence.Configuration
{
    internal class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder) 
        {
            builder.HasKey(c => c.Id);

            builder.Property(c=> c.ChatName).HasMaxLength(200).HasDefaultValue("New Chat").IsRequired();

            builder.Property(c => c.Model).HasMaxLength(50).HasDefaultValue("claude-haiku-4-5").IsRequired();

            builder.HasOne(c=> c.User).WithMany(c => c.Chats).HasForeignKey(c=>c.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.UserId);
            builder.HasIndex(c => c.UpdatedAt);

            builder.HasQueryFilter(c => !c.IsDeleted);
            
        }
    }
}
