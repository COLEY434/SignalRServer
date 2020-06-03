using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities.EntityConfigurations
{
    //public class PrivateMessageConfiguration : IEntityTypeConfiguration<PrivateMessage>
    //{
    //    public void Configure(EntityTypeBuilder<PrivateMessage> builder)
    //    {
    //        builder.HasOne(c => c.Chat).WithMany(c => c.PrivateMessages).HasForeignKey(c => c.PrivateChatId).IsRequired(true);
    //        builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId).IsRequired(true);
    //        builder.HasKey(c => c.Id);
    //        builder.Property(c => c.Text).IsRequired(true).IsUnicode(true);
    //        builder.Property(c => c.CreationDate).IsRequired(true);
    //    }
    //}
}
