using webAPI.Repositories.Interfaces;
using webAPI.Repositories;

namespace webAPI.Extensions
{
    public static class RepositoryInjectionExtension
    {
        /// <summary>
        /// Adds all repositories to the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddRepositories(this IServiceCollection services)
        {
            // Add all repositories here
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();

            // Add more repositories as needed
        }
    }
}
