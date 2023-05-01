using BusinessLayer.DTOs;

namespace BusinessLayer.Validator
{
    public interface IValidator
    {
        /// <summary>
        /// Validates the purchase order object, throws ArgumentException when validation fails.
        /// </summary>
        /// <param name="purchaseOrder"></param>
        void Validate(PurchaseOrderCreateDTO purchaseOrder);
        /// <summary>
        /// Validates the customer object, throws ArgumentException when validation fails.
        /// </summary>
        /// <param name="purchaseOrder"></param>
        void Validate(CustomerCreateDTO customer);
    }
}
