using BusinessLayer.DTOs;

namespace BusinessLayer.Validator
{
    public interface IValidator
    {
        void Validate(PurchaseOrderCreateDTO purchaseOrder);
        void Validate(CustomerCreateDTO customer);
    }
}
