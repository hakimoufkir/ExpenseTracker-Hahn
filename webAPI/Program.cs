using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using webAPI.Data;
using webAPI.Extensions;
using webAPI.Repositories.Interfaces;
using webAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using webAPI.Models;
using webAPI.Profiles;

namespace webAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register HttpContextAccessor for accessing the current HTTP context
            builder.Services.AddHttpContextAccessor();

            // Register the generic repository for all entities
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //mapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add custom repository and service injections
            builder.Services.AddRepositories(); // Inject all repositories
            builder.Services.AddServices();     // Inject all services
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultSQLConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)
                ));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(options =>
                 {
                     options.Cookie.HttpOnly = true;
                     options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Always for HTTPS
                     options.Cookie.SameSite = SameSiteMode.None; // Required for cross-origin cookies
                     options.Events.OnRedirectToLogin = context =>
                     {
                         context.Response.StatusCode = 401; // Return 401 instead of redirecting
                         return Task.CompletedTask;
                     };
                 });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", policy =>
                    policy
                        .WithOrigins("http://localhost:4200") // Replace with your frontend URL
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()); // Allow cookies and credentials
            });

            var app = builder.Build();

            // Apply migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
