using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class PurchaseOrderCreateDTO
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public IList<ItemCreateDTO>? Items { get; set; }
    }
}
