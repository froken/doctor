using Doctor.Database;
using Doctor.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctor.Api.Controllers
{
    [Route( "api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager; 

        public AuthController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); 
            }

            var result = await _signInManager.PasswordSignInAsync(
                userName: login.UserName,
                password: login.Password,
                isPersistent: false, 
                lockoutOnFailure: false
            );

            if (result.RequiresTwoFactor)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
            if (result.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status423Locked);
            }
            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }

        // POST: /api/auth/logout
        [Authorize, HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
