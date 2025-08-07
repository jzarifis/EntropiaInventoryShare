
namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class MedicalTool
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public MedicalToolProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class MedicalToolProperties
    {
        public double Weight { get; set; }
        public double MaxHeal { get; set; }
        public double MinHeal { get; set; }
        public double UsesPerMinute { get; set; }
        public MedicalToolEconomy Economy { get; set; }
    }

    public class MedicalToolEconomy
    {
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
    }

}
