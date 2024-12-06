using webAPI.Services.interfaces;
using webAPI.Services;

namespace webAPI.Extensions
{
    public static class ServiceInjectionExtension
    {
        /// <summary>
        /// Adds all services to the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddServices(this IServiceCollection services)
        {
            // Add all services here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            // Add more services as needed
        }
    }
}
