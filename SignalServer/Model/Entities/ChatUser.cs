using SignalServer.Model.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class ChatUser
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }

        public UserRole Role { get; set; }
    }
}
