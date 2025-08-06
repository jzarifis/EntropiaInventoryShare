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

        public string Type { get; set; } = null!;

        public string AmmoUsed1 { get; set; } = null!;

        public string AmmoUsed2 { get; set; } = null!;

        public float? Decay { get; set; } = null!;

        public float? AmmoPerUse { get; set; } = null!;

        public float? MarkUp { get; set; } = null!;

    }
}
