using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntropiaInventoryShareWeb.Entities
{
    public class Avatar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset LastAccess { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? LicenseExpiration { get; set; }

        public required string AvatarName { get; set; }

        public List<InventorySharedItem> SharedItems { get; set; }
    }
}
