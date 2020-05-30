using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class RoomMessages
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public int RoomId { get; set; }

        public string SenderId { get; set; }

        public ConversationRoom Room { get; set; }
        public ApplicationUser Sender { get; set; }

    }
}
