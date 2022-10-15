using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class UserItemEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("item_id")]
        public Guid ItemId { get; set; }
        [Column("received")]
        public bool Received { get; set; }
    }
}
