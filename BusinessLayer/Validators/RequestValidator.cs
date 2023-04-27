using BusinessLayer.DTOs;
using BusinessLayer.Messages;
using BusinessLayer.Validators;
using DataAccessLayer.Data;
using System.Text;

namespace BusinessLayer.Validator
{
    public class RequestValidator : IValidator
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IItemRepository _ItemRepository;

        public RequestValidator(ICustomerRepository customerRepository, IItemRepository itemRepository)
        {
            _CustomerRepository = customerRepository;
            _ItemRepository = itemRepository;
        }
        public void Validate(PurchaseOrderCreateDTO purchaseOrder)
        {
            ValidatePurchaseOrderInputs(purchaseOrder);
            ValidateCustomer(purchaseOrder.CustomerId);
            ValidateItems(purchaseOrder.Items!);
        }

        public void Validate(CustomerCreateDTO customer)
        {
        }
        public void ValidateCustomer(Guid customerId)
        {
            if (_CustomerRepository.CustomerExists(customerId) == false)
                throw new ArgumentException(Message.CustomerDoesNotExist);
        }
        public void ValidateItems(IEnumerable<ItemCreateDTO> items)
        {
            foreach(var item in items)
            {
                if (_ItemRepository.ItemExists(item.ItemId) == false)
                    throw new ArgumentException(Message.ItemDoesNotExist);
            } 
        }
        public void ValidatePurchaseOrderInputs(PurchaseOrderCreateDTO purchaseOrder)
        {
            var errors = new StringBuilder();
            var validator = new PurchaseOrderValidator();
            var result = validator.Validate(purchaseOrder);
            if (result.IsValid == false)
            {
                foreach (var failure in result.Errors)
                    errors.AppendLine(failure.ErrorMessage);
                throw new ArgumentException(errors.ToString());
            }
        }

    }
}
