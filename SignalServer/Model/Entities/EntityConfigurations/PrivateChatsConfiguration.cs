using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities.EntityConfigurations
{
    public class PrivateChatsConfiguration : IEntityTypeConfiguration<PrivateChats>
    {
        public void Configure(EntityTypeBuilder<PrivateChats> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.PrivateMessages).WithOne(c => c.Chat);
            builder.HasOne(c => c.Creator).WithMany().HasForeignKey(c => c.CreatorUserId).IsRequired(true);
            builder.HasOne(c => c.Receiver).WithMany().HasForeignKey(c => c.ReceiverUserId).IsRequired(true);
        }
    }
}
