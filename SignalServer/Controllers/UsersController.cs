using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalServer.Extensions;
using SignalServer.Model;

namespace SignalServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var userId = HttpContext.GetUserId();
            var Users = await _dbContext.Users.AsNoTracking().Where(x => x.Id != userId).ToListAsync();

            if(Users == null)
            {
                return NotFound();
            }

            return Ok(new { AllUsers = Users });
        }
    }
}