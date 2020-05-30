using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class RoomMembers
    {
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int RoomId { get; set; }

        public ConversationRoom Room { get; set; }

        public Enum MemberType { get; set; }
    }
}
