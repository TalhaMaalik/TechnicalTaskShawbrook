using BusinessLayer.Models.Base;
using BusinessLayer.Models.Membership;
using BusinessLayer.Processors.Services;
using System.Text;

namespace BusinessLayer.Processor
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly IItemService _IItemService;

        public PurchaseOrderProcessor(IItemService itemService)
        {
            _IItemService = itemService;
        }
        public string Process(PurchaseOrder purchaseOrder)
        {
            StringBuilder stringBuilder = new();
            foreach(var item in purchaseOrder.Items)
            {
                if (item is Membership membershipItem)
                    _IItemService.ProcessMembership(membershipItem);
                else if (item is PhysicalProduct PhysicalProductItem)
                    _IItemService.ProcessPhysicalProduct(stringBuilder, PhysicalProductItem);
                else
                    _IItemService.ProcessItem(item);
            }
            return stringBuilder.ToString();
        }
    }
}
