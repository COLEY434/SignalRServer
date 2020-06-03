﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class message
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
