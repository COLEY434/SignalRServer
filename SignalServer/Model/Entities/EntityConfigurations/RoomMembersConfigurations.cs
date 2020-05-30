using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities.EntityConfigurations
{
    public class RoomMembersConfigurations : IEntityTypeConfiguration<RoomMembers>
    {
        public void Configure(EntityTypeBuilder<RoomMembers> builder)
        {
            builder.HasKey(c => new { c.RoomId, c.UserId });
            builder.HasOne(c => c.User).WithMany(c => c.UserRooms).HasForeignKey(c => c.UserId).IsRequired(true);
            builder.HasOne(c => c.Room).WithMany(c => c.Members).HasForeignKey(c => c.RoomId).IsRequired(true);

        }
    }
}
