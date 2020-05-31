using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Resource.Request
{
    public class PrivateMessageRequest
    {
        public string ReceiverId { get; set; }

        public int PrivateChatId { get; set; }

        public string Text { get; set; }
    }
}
