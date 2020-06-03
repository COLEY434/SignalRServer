using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
namespace SignalServer.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        //[HttpPost("[action]/{ConnectionId}/{chatId}")]
        //public async Task<IActionResult> JoinRoom(string ConnectionId, string chatId)
        //{
        //    await _hubContext.Groups.AddToGroupAsync(ConnectionId, chatId);
        //    return Ok();
        //}

        //[HttpPost("[action]/{ConnectionId}/{chatId}")]
        //public async Task<IActionResult> LeaveRoom(string ConnectionId, string chatId)
        //{
        //    await _hubContext.Groups.RemoveFromGroupAsync(ConnectionId, chatId);
        //    return Ok();
        //}

        [HttpPost("{chatId}/send-message")]
        public async Task<IActionResult> SendMessage([FromRoute]int chatId, [FromBody]string message, [FromServices] ApplicationDbContext _context)
        {
            var userIds = await _context.Chats
                                        .Include(x => x.Users)
                                        .Where(x => x.Id == chatId).Select(x => x.Users).FirstOrDefaultAsync();

            var Id1 = userIds[0].UserId.ToString();
            var Id2 = userIds[1].UserId.ToString();

            var Message = new message
            {
                ChatId = chatId,
                Name = HttpContext.GetUsername(),
                Text = message,
                TimeStamp = DateTime.Now
            };

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            
            await _hubContext.Clients.Users(Id1, Id2).SendAsync("ReceiveMessage", new
            {
                Text = Message.Text,
                Name = HttpContext.GetUsername(),
            TimeStamp = Message.TimeStamp.ToString("dd/MM/yyyy hh:mm:ss tt")
            }); ;
            return Ok();


        }
    }
}