using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class UserPerkEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("perk_id")]
        public Guid PerkId { get; set; }
    }
}
