using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalServer.Model.Entities;
using SignalServer.Resource.Request;
using SignalServer.Resource.Response;

namespace SignalServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var user = await _userManager.FindByEmailAsync(registerModel.Email);

            if(user != null)
            {
                return BadRequest("User with this email already exist");
            }

            var NewUser = new ApplicationUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                CreationDate = DateTime.Now
            };
            try
            {
               var Result =  await _userManager.CreateAsync(NewUser, registerModel.Password);

                if (Result.Succeeded)
                {
                    var JwtToken = GenerateToken(NewUser);
                    return Ok(new RegistrationResponseModel 
                    { 
                        UserId = NewUser.Id,
                        Token = JwtToken                
                    });
                }
                
                return StatusCode(400, "Something went wrong");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                return BadRequest("User with this email does not exist");
            }

            if(!await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return BadRequest("Incorrect Password");
            }

            var AppUser = new ApplicationUser
            {
                Email = loginModel.Email,
                UserName = loginModel.Email,
                CreationDate = DateTime.Now
            };

            var JwtToken = GenerateToken(AppUser);
            return Ok(new RegistrationResponseModel
            {
                UserId = user.Id,
                Token = JwtToken
            });
        }

        private string GenerateToken(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                  new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                  new Claim("id", user.Id),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 
              }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:JwtIssuer"],
                Audience = _configuration["JwtSettings:JwtAudience"],
                Expires = DateTime.Now.AddHours(2)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);



        }
    }
}