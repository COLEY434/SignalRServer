using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalServer.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreationDate { get; set; }

        public ICollection<RoomMembers> UserRooms { get; set; }
    }
}
