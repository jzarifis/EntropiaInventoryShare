namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class Absorber
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public AbsorberProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class AbsorberProperties
    {
        public AbsorberEconomy Economy { get; set; }
    }

    public class AbsorberEconomy
    {
        public double Efficiency { get; set; }
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Absorption { get; set; }
    }


}
