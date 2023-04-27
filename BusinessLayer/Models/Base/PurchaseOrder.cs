namespace BusinessLayer.Models.Base
{
    public class PurchaseOrder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public IList<Item> Items { get; } = new List<Item>();
    }
}
