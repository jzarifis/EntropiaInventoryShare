namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class TeleportationChip : AmmoContainer
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public TeleportationChipProperties Properties { get; set; }
    }

    public class TeleportationChipProperties
    {
        public double Weight { get; set; }
        public double UsesPerMinute { get; set; }
        public double Range { get; set; }
        public TeleportationChipEconomy Economy { get; set; }
    }



    public class TeleportationChipEconomy
    {
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
        public double AmmoBurn { get; set; }
    }



}
