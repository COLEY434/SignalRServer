using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class PrivateChats
    {
        public PrivateChats()
        {
            PrivateMessages = new Collection<PrivateMessage>();
        }
        public int Id { get; set; }

        public string CreatorUserId { get; set; }

        public string ReceiverUserId { get; set; }

        public DateTime Creation_Date { get; set; }

        public ApplicationUser Creator { get; set; }

        public ApplicationUser Receiver { get; set; }
        public ICollection<PrivateMessage> PrivateMessages { get; set; }
    }
}
