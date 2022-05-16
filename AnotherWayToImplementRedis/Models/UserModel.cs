namespace AnotherWayToImplementRedis.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(Name);
        }
    }
}
