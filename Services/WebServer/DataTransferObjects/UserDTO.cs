using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects
{
    public class UserDTO
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string ip { get; set; }
        public int? lives { get; set; }
    }
}
