namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{

    public class WeaponVisionAttachment
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public WeaponVisionAttachmentProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class WeaponVisionAttachmentProperties
    {
        public WeaponVisionAttachmentEconomy Economy { get; set; }
    }

    public class WeaponVisionAttachmentEconomy
    {
        public double Efficiency { get; set; }
        public double MaxTT { get; set; }
        public double MinTT { get; set; }
        public double Decay { get; set; }
    }


}
