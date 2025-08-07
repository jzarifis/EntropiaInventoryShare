using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntropiaInventoryShareWeb.Entities
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public required string Name { get; set; }
       
        public string? EntropiaNexusId { get; set; }

        public string? Type { get; set; }

        public double? Weight { get; set; }

        public double? Value { get; set; }

        public string? Link { get; set; }

        public List<InventorySharedItem> SharedItems { get; set; }

    }
}
