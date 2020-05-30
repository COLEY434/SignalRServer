using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Resource.Request
{
    public class RoomMessageRequest
    {
        public int RoomId { get; set; }

        public string Message { get; set; }

        public string SenderId { get; set; }
    }
}
