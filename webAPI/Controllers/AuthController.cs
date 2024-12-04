using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAPI.Data;
using webAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace webAPI.Controllers
{
    /// <summary>
    /// Controller for managing authentication-related operations.
    /// </summary>
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="signUpRequest">The registration details including email, name, and password.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost("signup")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest signUpRequest)
        {
            try
            {
                if (_context.Users.Any(x => x.Email == signUpRequest.Email))
                {
                    return BadRequest(new Response(false, "Email is already registered."));
                }

                var user = new User
                {
                    Email = signUpRequest.Email,
                    Name = signUpRequest.Name,
                };

                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, signUpRequest.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new Response(true, "User registered successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Signs in an existing user and establishes a session.
        /// </summary>
        /// <param name="signInRequest">The user's credentials including email and password.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPost("signin")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == signInRequest.Email);
                if (user == null)
                {
                    return BadRequest(new Response(false, "Invalid credentials."));
                }

                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, signInRequest.Password);
                if (result != PasswordVerificationResult.Success)
                {
                    return BadRequest(new Response(false, "Invalid credentials."));
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    });

                return Ok(new Response(true, "Signed in successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Retrieves the claims of the currently signed-in user.
        /// </summary>
        /// <returns>A list of user claims.</returns>
        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(typeof(List<UserClaim>), 200)]
        [ProducesResponseType(typeof(Response), 401)]
        [ProducesResponseType(typeof(Response), 500)]
        public IActionResult GetUser()
        {
            try
            {
                var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();
                return Ok(userClaims);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }

        /// <summary>
        /// Signs out the currently authenticated user.
        /// </summary>
        /// <returns>A response indicating success.</returns>
        [Authorize]
        [HttpGet("signout")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok(new Response(true, "Signed out successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response(false, $"An error occurred: {ex.Message}"));
            }
        }
    }
}
