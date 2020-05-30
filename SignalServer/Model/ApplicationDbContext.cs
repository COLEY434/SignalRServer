using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalServer.Model.Entities;
using SignalServer.Model.Entities.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ConversationRoom> ConversationRooms { get; set; }
        public DbSet<PrivateChats> PrivateChats { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<RoomMembers> RoomMembers { get; set; }
        public DbSet<RoomMessages> RoomMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ConversationRoomConfiguration());
            builder.ApplyConfiguration(new PrivateChatsConfiguration());
            builder.ApplyConfiguration(new PrivateMessageConfiguration());
            builder.ApplyConfiguration(new RoomMembersConfigurations());
            builder.ApplyConfiguration(new RoomMessagesConfigurations());


        }
    }
}
