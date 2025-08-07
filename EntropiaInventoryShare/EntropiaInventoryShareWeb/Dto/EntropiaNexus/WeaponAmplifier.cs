namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{

    public class WeaponAmplifier
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public WeaponAmplifierProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class WeaponAmplifierProperties
    {
        public double Weight { get; set; }
        public string Type { get; set; }
        public WeaponAmplifierEconomy Economy { get; set; }
    }

    public class WeaponAmplifierEconomy
    {
        public double Efficiency { get; set; }
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
        public double AmmoBurn { get; set; }
    }



}
