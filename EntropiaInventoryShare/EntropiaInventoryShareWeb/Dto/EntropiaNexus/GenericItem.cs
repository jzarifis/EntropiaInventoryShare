namespace EntropiaInventoryShareWeb.Dto.EntropiaNexus
{
    public class GenericItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GenericItemProperties Properties { get; set; }
        public Links Links { get; set; }
    }

    public class GenericItemProperties
    {
        public string Type { get; set; }
        public double Weight { get; set; }
        public Economy Economy { get; set; }
    }

    public class Economy
    {
        public double Value { get; set; }
    }

    public class Links
    {
        public string Url { get; set; }
    }

    public class Ammo
    {
        public string Name { get; set; }
        public Links Links { get; set; }
    }


}
