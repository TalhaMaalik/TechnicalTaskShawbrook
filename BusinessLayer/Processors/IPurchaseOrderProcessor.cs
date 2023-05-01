using BusinessLayer.Models.Base;

namespace BusinessLayer.Processor
{
    public interface IPurchaseOrderProcessor
    {
        /// <summary>
        /// Responsible for processing a purchase order. If any issues arise during the process, an ArgumentException will be thrown
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns>ShippingSlip</returns>
        string Process(PurchaseOrder purchaseOrder);
    }
}
