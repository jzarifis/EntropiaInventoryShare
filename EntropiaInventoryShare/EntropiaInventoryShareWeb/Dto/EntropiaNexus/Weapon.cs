namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class Weapon : AmmoContainer
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public WeaponProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class WeaponProperties
    {
        public double Weight { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Class { get; set; }
        public double UsesPerMinute { get; set; }
        public double Range { get; set; }
        public WeaponEconomy Economy { get; set; }

    }

    public class WeaponEconomy
    {
        public double Efficiency { get; set; }
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
        public double AmmoBurn { get; set; }
    }


}
