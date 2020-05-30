using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class PrivateMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public int PrivateChatId { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public PrivateChats Chat { get; set; }
        public ApplicationUser Sender { get; set; }

        public ApplicationUser Receiver { get; set; }
    }
}
