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

        public DbSet<Chat> Chats { get; set; }
        public DbSet<message> Messages { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatUser>()
                    .HasKey(chatuser => new { chatuser.ChatId, chatuser.UserId });




            //builder.ApplyConfiguration(new PrivateChatUsersConfigurations());
            //builder.ApplyConfiguration(new ConversationRoomConfiguration());
            //builder.ApplyConfiguration(new PrivateChatsConfiguration());
            //builder.ApplyConfiguration(new PrivateMessageConfiguration());
            //builder.ApplyConfiguration(new RoomMembersConfigurations());
            //builder.ApplyConfiguration(new RoomMessagesConfigurations());


        }
    }
}
