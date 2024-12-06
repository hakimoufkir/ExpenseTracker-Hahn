namespace webAPI.Services.interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);

    }
}
