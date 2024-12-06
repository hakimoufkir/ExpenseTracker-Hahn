using webAPI.Data;
using webAPI.Models;

namespace webAPI.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>  
    {
        Task<List<User>> GetAllUsersWithDetailsAsync();

    }

}
