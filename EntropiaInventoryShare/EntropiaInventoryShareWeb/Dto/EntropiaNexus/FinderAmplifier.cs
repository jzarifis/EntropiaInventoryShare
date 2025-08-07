namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{

    public class FinderAmplifier
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public FinderAmplifierProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class FinderAmplifierProperties
    {

        public FinderAmplifierEconomy Economy { get; set; }
    }

    public class FinderAmplifierEconomy
    {
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
    }


}
