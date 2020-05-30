using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class ConversationRoom
    {
        public ConversationRoom()
        {
            Members = new Collection<RoomMembers>();
            Messages = new Collection<RoomMessages>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<RoomMembers> Members { get; set; }

        public ICollection<RoomMessages> Messages { get; set; }
    }
}
