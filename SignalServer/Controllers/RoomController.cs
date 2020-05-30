using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalServer.Extensions;
using SignalServer.Hubs;
using SignalServer.Model;
using SignalServer.Model.Entities;
using SignalServer.Resource.Request;

namespace SignalServer.Controllers
{
    [Route("api/room")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<ChatHub> _hubContext;

        public RoomController(ApplicationDbContext dbContext, IHubContext<ChatHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("create-room")]
        public async Task<IActionResult> CreateRoomAsync(string RoomName)
        {
            var UserId = HttpContext.GetUserId();
            var NewRoom = new ConversationRoom
            {
                Name = RoomName,
                CreationDate = DateTime.Now,
                CreatedBy = UserId
            };
            try
            {
                NewRoom.Members.Add(new RoomMembers
                {
                    UserId = UserId,
                    MemberType = MemberType.Admin,
                });

                _dbContext.ConversationRooms.Add(NewRoom);
                await _dbContext.SaveChangesAsync();

                return Ok(new { Success = true, Message = $"You created room {NewRoom.Name}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        [Route("join-room/{RoomId}/{ConnectionId}")]
        public async Task<IActionResult> JoinRoomAsync(int RoomId, string ConnectionId)
        {
            var UserId = HttpContext.GetUserId();

            //check first if user already exist in a room
            var UserExist = await _dbContext.RoomMembers.Where(x => x.UserId == UserId && x.RoomId == RoomId).SingleOrDefaultAsync();

            if(UserExist == null)
            {
                var NewRoomUSer = new RoomMembers
                {
                    RoomId = RoomId,
                    UserId = UserId,
                    MemberType = MemberType.Member
                };

                try
                {
                    _dbContext.RoomMembers.Add(NewRoomUSer);
                    await _dbContext.SaveChangesAsync();
                    await _hubContext.Groups.AddToGroupAsync(ConnectionId, RoomId.ToString());
                    return Ok(new { Success = true });


                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            await _hubContext.Groups.AddToGroupAsync(ConnectionId, RoomId.ToString());
            return Ok(new { Success = true });






        }
        
        [HttpPost]
        [Route("room-message/send")]

        public async Task<IActionResult> SendMessageAsync([FromBody] RoomMessageRequest request)
        {
            var NewMessage = new RoomMessages
            {
                Text = request.Message,
                SenderId = request.SenderId,
                RoomId = request.RoomId,
                CreationDate = DateTime.Now
            };

            try
            {
                _dbContext.RoomMessages.Add(NewMessage);
                await _dbContext.SaveChangesAsync();

                await _hubContext.Clients.Group(request.RoomId.ToString()).SendAsync("DisplayGroupMessage", new
                {
                    text = NewMessage.Text,
                    creationDate = NewMessage.CreationDate.ToString("dd-MM-yyyy hh:mm tt")
                });
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           

            
        }
    
        [HttpGet]
        [Route("get-all-rooms")]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            var AllRooms = await _dbContext.ConversationRooms.AsNoTracking().ToListAsync();

            if(AllRooms == null)
            {
                return NotFound();
            }

            return Ok(new { Rooms = AllRooms });
        }

        [HttpGet]
        [Route("get-user-rooms")]
        public async Task<IActionResult> GetUsersRoomsAsync()
        {
            var UserId = HttpContext.GetUserId();
            var AllRooms = await _dbContext.RoomMembers
                                           .Include(x => x.Room)
                                           .Where(x => x.UserId == UserId).Select(x => x.Room).ToListAsync();

            if (AllRooms == null)
            {
                return NotFound();
            }

            return Ok(new { Rooms = AllRooms });
        }
    }
}