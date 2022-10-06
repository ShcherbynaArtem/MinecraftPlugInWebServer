namespace Entities
{
    public class UserEntity
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string ip { get; set; }
        public int lives { get; set; }
    }
}