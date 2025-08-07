namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{

    public class MedicalChip : AmmoContainer
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public MedicalChipProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class MedicalChipProperties
    {
        public double Range { get; set; }
        public double Weight { get; set; }
        public double MaxHeal { get; set; }
        public double MinHeal { get; set; }
        public double UsesPerMinute { get; set; }
        public MedicalChipEconomy Economy { get; set; }
    }

    public class MedicalChipEconomy
    {
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
        public double AmmoBurn { get; set; }
    }


}
