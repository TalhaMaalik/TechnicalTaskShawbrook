using BusinessLayer.Models.Base;

namespace BusinessLayer.Processor
{
    public interface IPurchaseOrderProcessor
    {
        string Process(PurchaseOrder purchaseOrder);
    }
}
