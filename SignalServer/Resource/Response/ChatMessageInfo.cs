using SignalServer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Resource.Response
{
    public class ChatMessageInfo
    {
        public int chatId { get; set; }

        public bool success { get; set; }

        public List<chatMessages> Messages { get; set; }
    }
}
