using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntropiaInventoryShareWeb.Entities
{
    public class InventorySharedItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public long ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }

        public double Value { get; set; }

        public string Container { get; set; }

        public bool InAuction { get; set; }

        public bool InShop { get; set; }

        public long AvatarId { get; set; }

        public Avatar Avatar { get; set; }
        
        public double? PerItemPrice { get; set; }

        public double? MarkupPercentage { get; set; }

        public double? MarkupAddToTT { get; set; }

        public double? Tier { get; set; }

        public int? TIR { get; set; }

        public string? Shop { get; set; }

        public string? ExtraData { get; set; }
    }
}
