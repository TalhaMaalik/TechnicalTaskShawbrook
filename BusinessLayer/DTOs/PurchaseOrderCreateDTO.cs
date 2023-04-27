namespace BusinessLayer.DTOs
{
    public class PurchaseOrderCreateDTO
    {
        public Guid CustomerId { get; set; }
        public IList<ItemCreateDTO>? Items { get; set; }
    }
}
