using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DataModel
{
    [Table("Item")]
    public class ItemModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
