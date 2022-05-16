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
                options.Configuration = configurations.Connection;
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    ConnectTimeout = configurations.ConnectTimeout,                                        
                    ConnectRetry = configurations.ConnectRetry
                };
            });
        }
    }
}
