using System.Security.Claims;
using System.Threading.Tasks;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace RealtimeChat.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (await _userService.RegisterUser(userForRegistration, ModelState))
                return StatusCode(201);
            
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            if (await _userService.ValidateUser(userForAuthentication))
            {
                return Ok(new
                {
                    Token = await _userService.CreateToken(),
                    Username = _userService.User.UserName
                });
            }

            return Unauthorized();
        }

        /*[HttpGet("username"), Authorize]
        public IActionResult Result()
        {
            return Ok(User.Identity.Name);
        }*/
        
    }
}