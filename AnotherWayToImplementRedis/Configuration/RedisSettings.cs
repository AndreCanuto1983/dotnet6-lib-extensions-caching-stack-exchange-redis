using AnotherWayToImplementRedis.Models.Configuration;
using StackExchange.Redis;

namespace AnotherWayToImplementRedis.Configuration
{
    public static class RedisSettings
    {
        public static void Redis(this WebApplicationBuilder builder)
        {
            var section = builder.Configuration.GetSection("RedisSettings");
            var configurations = section.Get<RedisOptions>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = configurations.Instance;
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { configurations.Connection },
                    AbortOnConnectFail = false,
                    ReconnectRetryPolicy = new LinearRetry(1500),
                    ConnectRetry = configurations.ConnectRetry,
                    ConnectTimeout = configurations.ConnectTimeout,
                    AsyncTimeout = 10000,
                    Ssl = false,
                    DefaultDatabase = 0
                };
            });
        }
    }
}
