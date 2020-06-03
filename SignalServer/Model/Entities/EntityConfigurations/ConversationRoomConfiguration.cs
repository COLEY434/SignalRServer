using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities.EntityConfigurations
{
    //public class ConversationRoomConfiguration : IEntityTypeConfiguration<ConversationRoom>
    //{
    //    public void Configure(EntityTypeBuilder<ConversationRoom> builder)
    //    {
    //        builder.HasKey(c => c.Id);
    //        builder.Property(c => c.Name).HasMaxLength(20).IsRequired(true);
    //        builder.Property(c => c.CreationDate).IsRequired();
    //        builder.HasOne(c => c.Creator).WithMany().HasForeignKey(c => c.CreatedBy).IsRequired(true);
    //        builder.HasMany(c => c.Members).WithOne(c => c.Room);
    //        builder.HasMany(c => c.Messages).WithOne(c => c.Room);
    //    }
    //}
}
