using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities.EntityConfigurations
{
    //public class RoomMessagesConfigurations : IEntityTypeConfiguration<RoomMessages>
    //{
    //    public void Configure(EntityTypeBuilder<RoomMessages> builder)
    //    {
    //        builder.HasKey(c => c.Id);
    //        builder.Property(c => c.Text).IsRequired(true).IsUnicode(true);
    //        builder.Property(c => c.CreationDate).IsRequired(true);
    //        builder.HasOne(c => c.Room).WithMany(c => c.Messages).HasForeignKey(c => c.RoomId).IsRequired(true);
    //        builder.HasOne(c => c.Sender).WithMany().HasForeignKey(c => c.SenderId).IsRequired(true);
                
    //    }
    //}
}
