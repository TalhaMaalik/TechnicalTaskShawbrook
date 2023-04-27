using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Visitor;
using System.Text;

namespace BusinessLayer.Processor
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly IItemVisitor ItemVisitor;

        public PurchaseOrderProcessor(IItemVisitor itemService)
        {
            ItemVisitor = itemService;
        }
        public string Process(PurchaseOrder purchaseOrder)
        {
            foreach(var item in purchaseOrder.Items)
                item.Accept(ItemVisitor);
            return ItemVisitor.ShippingSlip.ToString();
        }
    }
}
