namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{

    public class Stimulant
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public StimulantProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class StimulantProperties
    {
        public double Weight { get; set; }
        public string Type { get; set; }
        public StimulantEconomy Economy { get; set; }
    }

    public class StimulantEconomy
    {
        public double MaxTT { get; set; }
    }



}
