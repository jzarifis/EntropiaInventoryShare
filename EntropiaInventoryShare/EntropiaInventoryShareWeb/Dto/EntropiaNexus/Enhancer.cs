namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class Enhancer
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public EnhancerProperties Properties { get; set; }
    }

    public class EnhancerProperties
    {
        public double Weight { get; set; }
        public int Socket { get; set; }
        public string Tool { get; set; }
        public string Type { get; set; }
        public EnhancerEconomy Economy { get; set; }
    }

    public class EnhancerEconomy
    {
        public double MaxTT { get; set; }
    }



}
