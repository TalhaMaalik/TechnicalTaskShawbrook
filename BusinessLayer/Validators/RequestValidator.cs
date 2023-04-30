using BusinessLayer.DTOs;
using BusinessLayer.Messages;
using BusinessLayer.Validators;
using DataAccessLayer.Data;
using FluentValidation.Results;
using Microsoft.Identity.Client;
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
            ValidateInputs(new PurchaseOrderValidator().Validate(purchaseOrder));
            ValidateCustomerId(purchaseOrder.CustomerId);
            ValidateItemsId(purchaseOrder.Items!);
        }

        public void Validate(CustomerCreateDTO customer)
        {
            if(customer == null)
               throw new ArgumentException(Message.PleaseProvideTheCustomerInTheRequest);
            ValidateCustomerEmailExists(customer.Email!);
            ValidateInputs(new CustomerValidator().Validate(customer));
        }

        public void ValidateCustomerEmailExists(string email)
        {
            if (_CustomerRepository.CustomerExistsByEmail(email))
                throw new ArgumentException(Message.CustomerWithEmailExistInSystem);
        }

        public void ValidateCustomerId(Guid customerId)
        {
            if (_CustomerRepository.CustomerExists(customerId) == false)
                throw new ArgumentException(Message.CustomerDoesNotExist);
        }
        public void ValidateItemsId(IEnumerable<ItemCreateDTO> items)
        {
            if (items == null || items.Count() == 0)
                throw new ArgumentException(Message.ItemListIsEmpty);
            foreach (var item in items)
            {
                if (_ItemRepository.ItemExists(item.ItemId) == false)
                    throw new ArgumentException(Message.ItemDoesNotExist);
            } 
        }

        private void ValidateInputs(ValidationResult result)
        {
            if (result.IsValid == false)
            {
                throw new ArgumentException(result.Errors.FirstOrDefault().ErrorMessage);
            }
        }

    }
}
