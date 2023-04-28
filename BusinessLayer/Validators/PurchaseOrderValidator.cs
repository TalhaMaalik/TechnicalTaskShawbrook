using BusinessLayer.DTOs;
using BusinessLayer.Messages;
using FluentValidation;

namespace BusinessLayer.Validators
{
    public class PurchaseOrderValidator : AbstractValidator<PurchaseOrderCreateDTO>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(p => p.CustomerId).NotNull().WithMessage(Message.CustomerDoesNotExist);
            RuleFor(p => p.Items).NotNull().WithMessage(Message.ItemAreNotGiven);
            RuleFor(p => p.Items).NotEmpty().WithMessage(Message.ItemListIsEmpty);
        }
    }
}
