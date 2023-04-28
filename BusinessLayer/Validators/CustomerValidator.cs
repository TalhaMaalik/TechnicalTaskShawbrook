using BusinessLayer.DTOs;
using BusinessLayer.Messages;
using FluentValidation;

namespace BusinessLayer.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerCreateDTO>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name).NotNull().WithMessage(Message.CustomerNameCannotBeNull);
            RuleFor(c => c.Name).NotEmpty().WithMessage(Message.CustomerNameCannotBeEmpty);
            RuleFor(c => c.Email).NotNull().WithMessage(Message.CustomerEmailCannotBeNull);
            RuleFor(c => c.Email).NotEmpty().WithMessage(Message.CustomerEmailCannotBeEmpty);
            RuleFor(c => c.Email).EmailAddress().WithMessage(Message.CustomerEmailIsNotAValidEmail);
        }
    }
}
