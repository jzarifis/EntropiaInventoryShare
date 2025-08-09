using EntropiaInventoryShareWeb.Dto.EntropiaNexus;

namespace EntropiaInventoryShareWeb.Dto
{
    public class InventoryItemDto
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Value { get; set; }

        public string Container { get; set; }

        public bool InAuction => Container == "AUCTION";

        public bool InShop => Container == "ESTATE" || Container == "PLAYER SHOP";

        public string? Type { get; set; } = null!;

        public bool HasTier => Type == "Weapon" || Type == "Armor" || Type == "MedicalChip";


        //public string AmmoUsed1 { get; set; } = null!;

        //public string AmmoUsed2 { get; set; } = null!;

        //public float? Decay { get; set; } = null!;

        //public float? AmmoPerUse { get; set; } = null!;

        //public float? MarkUp { get; set; } = null!;

        public bool Shared { get; set; }

        public double? MarkupAddToTT { get; set; }

        public double? MarkupPercentage { get; set; }

        public double? PerItemPrice { get; set; }

        public long? dbId { get; set; }

        public double? Tier { get; set; }

        public int? TIR { get; set; }

        public string? Shop { get; set; }

        public string? ExtraData { get; set; }
    }
}
