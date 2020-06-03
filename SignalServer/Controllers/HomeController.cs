using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalServer.Extensions;
using SignalServer.Model;
using SignalServer.Model.Entities;
using SignalServer.Model.Entities.enums;
using SignalServer.Resource.Response;

namespace SignalServer.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var chat = _context
                        .Chats.Include(x => x.Users)
                        .Where(x => !x.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
                        .ToList();
            return Ok(chat);
        }

        public IActionResult FindUser(string user)
        {
            var Users = _context.Users
                        .Where(x => x.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                        .ToList();

            return Ok(Users);
        }

        //public IActionResult Private()
        //{
        //    var chats = _context.Chats
        //        .Include(x => x.Users)
        //            .ThenInclude(x => x.User)
        //        .Where(x => x.Type == ChatType.Private && x.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //        .ToList();

        //    return Ok(chats);
        //}

        [HttpPost("{userId}/create-chat")]
        public async Task<IActionResult> CreateChat([FromRoute]string userId)
        {

            var chat = new Chat
            {
               created_at = DateTime.Now
            };
          
            chat.Users.Add(new ChatUser
            {
                UserId = userId
            });
     
            chat.Users.Add(new ChatUser
            {
                UserId = HttpContext.GetUserId()
            });

            try
            {
                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
                return Ok(new { Id = chat.Id, Success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
           
        }

        [HttpGet]
        [Route("chat/{Id}")]
        public async Task<IActionResult> Chat([FromRoute] int Id)
        {
            var Chat = await  _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id == Id);

            var messages = new List<chatMessages>();

            foreach (var mesage in Chat.Messages)
            {
                messages.Add(new chatMessages { Id = mesage.Id, Name = mesage.Name, Text = mesage.Text, TimeStamp = mesage.TimeStamp});
            }
            var chat = new ChatMessageInfo
            {
                chatId = Chat.Id,
                success = true,
                Messages = messages
            };

            return Ok(chat);
        }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var Message = new message
            {
                //Name = User.Identity.Name,
                ChatId = chatId,
                Text = message,
                TimeStamp = DateTime.Now
            };

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateRoom(string name)
        //{
        //    var chat = new Chat
        //    {
        //     //   Name = name,
        //        Type = ChatType.Room
        //    };

        //    try
        //    {
        //        chat.Users.Add(new ChatUser
        //        {
        //            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
        //            Role = UserRole.Admin
        //        });

        //        _context.Chats.Add(chat);


        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception e)
        //    {

        //    }



        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public async Task<IActionResult> JoinRoom(int Id)
        //{
        //    var chatUSer = new ChatUser
        //    {
        //        UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
        //        ChatId = Id,
        //        Role = UserRole.Member
        //    };


        //    _context.ChatUsers.Add(chatUSer);


        //    await _context.SaveChangesAsync();

        //    return Redirect($"/Chat/{Id}");
        //}
    }
}