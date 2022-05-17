using AnotherWayToImplementRedis.Models;

namespace AnotherWayToImplementRedis.Interfaces
{
    public interface IUserRepository
    {
        Task SetUserAsync(UserModel user, CancellationToken cancellationToken);
        Task<UserModel?> GetUserAsync(string key, CancellationToken cancellationToken);
        Task UpdateUserAsync(UserModel user, CancellationToken cancellationToken);
        Task DeleteUserAsync(string key, CancellationToken cancellationToken);
    }
}
