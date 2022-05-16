using AnotherWayToImplementRedis.Interfaces;
using AnotherWayToImplementRedis.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AnotherWayToImplementRedis.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            IDistributedCache distributedCache,
            ILogger<UserRepository> logger
            )
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task SetUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id, cancellationToken);

                if (userObject != null)
                    throw new InvalidOperationException("Existing user");

                await _distributedCache.SetStringAsync(
                    user.Id,
                    JsonSerializer.Serialize(user),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][SetUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task<UserModel?> GetUserAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(userId, cancellationToken);

                if (userObject == null)
                    return null;

                return JsonSerializer.Deserialize<UserModel>(userObject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][GetUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.Id, cancellationToken);

                if (userObject != null)
                    await _distributedCache.SetStringAsync(
                        user.Id,
                        JsonSerializer.Serialize(user),
                        cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][UpdateUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                await _distributedCache.RemoveAsync(userId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][DeleteUser] => EXCEPTION: {ex.Message}");
                throw;
            }
        }
    }
}
