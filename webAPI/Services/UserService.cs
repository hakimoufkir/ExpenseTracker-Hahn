using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using webAPI.Models;
using webAPI.Repositories.Interfaces;
using webAPI.Services.interfaces;
using webAPI.DTOs;
using AutoMapper;

namespace webAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<Response> RegisterUserAsync(SignUpRequest signUpRequest)
        {
            var existingUser = await _userRepository.GetAsNoTracking(u => u.Email == signUpRequest.Email);
            if (existingUser != null)
            {
                return new Response(false, "Email is already registered.");
            }

            var user = new User
            {
                Email = signUpRequest.Email,
                Name = signUpRequest.Name,
                Password = _passwordHasher.HashPassword(signUpRequest.Password)
            };

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new Response(true, "User registered successfully.");
        }

        public async Task<Response> AuthenticateUserAsync(SignInRequest signInRequest, HttpContext httpContext)
        {
            var user = await _userRepository.GetAsNoTracking(u => u.Email == signInRequest.Email);
            if (user == null)
            {
                return new Response(false, "Invalid credentials.");
            }

            var hashedPassword = user.Password;
            var isPasswordValid = _passwordHasher.HashPassword(signInRequest.Password) == hashedPassword;

            if (!isPasswordValid)
            {
                return new Response(false, "Invalid credentials.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Add `userId` as NameIdentifier
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });

            return new Response(true, "Signed in successfully.");
        }

        public async Task<List<UserClaim>> GetUserClaimsAsync(HttpContext httpContext)
        {
            try
            {
                var claims = httpContext.User.Claims
                    .Select(c => new UserClaim(c.Type, c.Value))
                    .ToList();

                return claims;
            }
            catch
            {
                return new List<UserClaim>();
            }
        }

        public async Task<Response> SignOutUserAsync(HttpContext httpContext)
        {
            try
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return new Response(true, "Signed out successfully.");
            }
            catch (Exception ex)
            {
                return new Response(false, $"An error occurred: {ex.Message}");
            }
        }
    }
}
