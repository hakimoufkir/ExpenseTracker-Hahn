using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webAPI.Models;
using webAPI.Services.interfaces;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest signUpRequest)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(signUpRequest);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        [HttpPost("signin")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            try
            {
                var result = await _userService.AuthenticateUserAsync(signInRequest, HttpContext);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(typeof(List<UserClaim>), 200)]
        [ProducesResponseType(typeof(Response), 401)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var claims = await _userService.GetUserClaimsAsync(HttpContext);

                // Include the `userId` claim explicitly if not present
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!claims.Any(c => c.Type == "userId") && userId != null)
                {
                    claims.Add(new UserClaim("userId", userId));
                }

                return Ok(claims);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        [Authorize]
        [HttpGet("signout")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                var result = await _userService.SignOutUserAsync(HttpContext);
                return result.Success ? Ok(result) : StatusCode(500, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }
    }
}
