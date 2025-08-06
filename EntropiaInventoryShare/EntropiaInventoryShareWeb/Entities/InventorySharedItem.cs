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

        public required string Name { get; set; }

        public int Quantity { get; set; }

        public double Value { get; set; }

        public required string Container { get; set; }

        public bool InAuction { get; set; }

        public bool InShop { get; set; }

        public required string Avatar { get; set; }

        public string? ExtraData { get; set; }
        
        public double? PerItemPrice { get; set; }

        public double? MarkupPercentage { get; set; }

        public double? MarkupAddToTT { get; set; }

    }
}
