namespace AnotherWayToImplementRedis.Models.Configuration
{
    public class RedisOptions
    {
        public string Instance { get; set; }
        public string Connection { get; set; }
        public int ConnectTimeout { get; set; }
        public int ConnectRetry { get; set; }
    }
}
