using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class Chat
    {
        public Chat()
        {
            Messages = new List<message>();
            Users = new List<ChatUser>();
        }
        public int Id { get; set; }

        public DateTime created_at { get; set; }
       
       // public ChatType Type { get; set; }

        public List<message> Messages { get; set; }
        public List<ChatUser> Users { get; set; }
    }
}
