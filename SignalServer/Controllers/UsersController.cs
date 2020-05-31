using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalServer.Model;

namespace SignalServer.Controllers
{
    [Route("api/users")]
    [ApiController]
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
            var Users = await _dbContext.Users.AsNoTracking().ToListAsync();

            if(Users == null)
            {
                return NotFound();
            }

            return Ok(new { AllUsers = Users });
        }
    }
}