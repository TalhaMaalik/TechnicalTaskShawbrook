using BusinessLayer.DTOs;
using FluentValidation;

namespace BusinessLayer.Validators
{
    public class PurchaseOrderValidator : AbstractValidator<PurchaseOrderCreateDTO>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(p => p.CustomerId).NotNull().NotEmpty();
            RuleFor(p => p.Items).NotNull().NotEmpty();
        }
    }
}
