using NordFish.Database.Enumes;

namespace NordFish.Services.UserServices
{
    public interface ICurrentUser
    {
        long? Id { get; }
        string? Email { get; }
        bool? IsAuthenticated { get; }
        bool? IsAdmin { get; }
        UserTypes? Role { get; }
    }
}
