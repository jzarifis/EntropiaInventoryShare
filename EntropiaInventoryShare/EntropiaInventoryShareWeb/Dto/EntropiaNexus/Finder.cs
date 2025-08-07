namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class Finder
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public FinderProperties Properties { get; set; }
        public Ammo Ammo = new Ammo() { Name = "Survey Probe" };
    }

    public class FinderProperties
    {
        public double Weight { get; set; }
        public double UsesPerMinute { get; set; }
        public double Depth { get; set; }
        public double Range { get; set; }
        public FinderEconomy Economy { get; set; }
    }

    public class FinderEconomy
    {
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
        public double AmmoBurn { get; set; }
    }


}
