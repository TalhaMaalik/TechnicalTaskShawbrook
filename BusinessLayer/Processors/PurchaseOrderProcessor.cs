using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Visitor;
using System.Text;

namespace BusinessLayer.Processor
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly IItemVisitor ItemProcessor;

        public PurchaseOrderProcessor(IItemVisitor itemProcessor)
        {
            ItemProcessor = itemProcessor;
        }

        public string Process(PurchaseOrder purchaseOrder)
        {
            ItemProcessor.CustomerId = purchaseOrder.CustomerId;
            foreach (var item in purchaseOrder.Items)
                item.Accept(ItemProcessor);
            ItemProcessor.OnClose();
            return ItemProcessor.ShippingSlip.ToString();
        }
    }
}
