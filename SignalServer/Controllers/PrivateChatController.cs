using System;
using System.Collections.Generic;
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
using SignalServer.Resource.Request;

namespace SignalServer.Controllers
{
    [Route("api/private-chat")]
    [ApiController]
    [Authorize]
    public class PrivateChatController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<ChatHub> _hubContext;

        public PrivateChatController(ApplicationDbContext dbContext, IHubContext<ChatHub> hubContext)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("send-private-message")]
        public async Task<IActionResult> SendPrivateMessageAsync([FromBody] PrivateMessageRequest request)
        {
            var SenderId = HttpContext.GetUserId();

            var NewPrivateMessage = new PrivateMessage
            {
                SenderId = SenderId,
                ReceiverId = request.ReceiverId,
                PrivateChatId = request.PrivateChatId,
                CreationDate = DateTime.Now,
                Text = request.Text
            };

            try
            {
                _dbContext.PrivateMessages.Add(NewPrivateMessage);
                await _dbContext.SaveChangesAsync();

                await _hubContext.Clients.Users(SenderId, request.ReceiverId).SendAsync("DisplayPrivateMessage", 
                                        new {
                                            Text = NewPrivateMessage.Text,
                                            CreationDate = NewPrivateMessage.CreationDate.ToString("dd-MM-yyyy hh:mm tt")
                                        });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("create/{ReceiverUserId}")]
        public async Task<IActionResult> CreatePrivateChatAsync(string ReceiverUserId)
        {
            var CreatorUserId = HttpContext.GetUserId();

            var CheckIfChatExist = await _dbContext.PrivateChats
                                                   .Where(x => x.CreatorUserId == CreatorUserId && x.ReceiverUserId == ReceiverUserId)
                                                   .SingleOrDefaultAsync();
            if(CheckIfChatExist == null)
            {
                var NewPrivatechat = new PrivateChats
                {
                    CreatorUserId = CreatorUserId,
                    ReceiverUserId = ReceiverUserId,
                    Creation_Date = DateTime.Now,

                };

                try
                {
                    _dbContext.PrivateChats.Add(NewPrivatechat);
                    await _dbContext.SaveChangesAsync();

                    return Ok(new { Success = true });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return Ok(new { Success = true });

        }
    
        [HttpGet("fetch")]
        public async Task<IActionResult> GetUserChatsAsync()
        {
            var UserId = HttpContext.GetUserId();
            var UsersChats = await _dbContext.PrivateChats
                                             .Include(x => x.Receiver)
                                             .Where(x => x.CreatorUserId == UserId).AsNoTracking().ToListAsync();

            if(UsersChats == null)
            {
                return NotFound();
            }

            return Ok(UsersChats);
        }

        [HttpGet("{PrivateChatId}/get-messages")]
        public async Task<IActionResult> GetUserChatsAsync(int PrivateChatId)
        {
            var PrivateMessages = await _dbContext.PrivateChats
                                             .Include(x => x.PrivateMessages)
                                             .Where(x => x.Id == PrivateChatId).AsNoTracking().Select(x => x.PrivateMessages).SingleOrDefaultAsync();

            if (PrivateMessages == null)
            {
                return NotFound();
            }

            return Ok(PrivateMessages);
        }
    }
}