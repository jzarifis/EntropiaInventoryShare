namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{

    public class Capsule
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public CapsuleProperties Properties { get; set; }
    }

    public class CapsuleProperties
    {
        public double Weight { get; set; }
        public double MinProfessionLevel { get; set; }
        public CapsuleEconomy Economy { get; set; }
    }

    public class CapsuleEconomy
    {
        public double MaxTT { get; set; }
    }



}
