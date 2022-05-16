using AnotherWayToImplementRedis.Models;

namespace AnotherWayToImplementRedis.Interfaces
{
    public interface IUserRepository
    {
        Task SetUserAsync(UserModel user, CancellationToken cancellationToken);
        Task<UserModel> GetUserAsync(string userId, CancellationToken cancellationToken);
        Task UpdateUserAsync(UserModel user, CancellationToken cancellationToken);
        Task DeleteUserAsync(string userId, CancellationToken cancellationToken);
    }
}
