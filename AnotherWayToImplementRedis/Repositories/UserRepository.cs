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
                var userObject = await _distributedCache.GetAsync(user.cpfCnpj, cancellationToken);

                if (userObject != null)
                    throw new InvalidOperationException("Existing user");

                await _distributedCache.SetStringAsync(
                    user.cpfCnpj,
                    JsonSerializer.Serialize(user),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][SetUserAsync] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task<UserModel?> GetUserAsync(string key, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(key, cancellationToken);

                if (userObject == null)
                    return null;

                return JsonSerializer.Deserialize<UserModel>(userObject);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][GetUserAsync] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userObject = await _distributedCache.GetAsync(user.cpfCnpj, cancellationToken);

                if (userObject != null)
                    await _distributedCache.SetStringAsync(
                        user.cpfCnpj,
                        JsonSerializer.Serialize(user),
                        cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][UpdateUserAsync] => EXCEPTION: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteUserAsync(string key, CancellationToken cancellationToken)
        {
            try
            {
                await _distributedCache.RemoveAsync(key, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UserRepository][DeleteUserAsync] => EXCEPTION: {ex.Message}");
                throw;
            }
        }
    }
}
