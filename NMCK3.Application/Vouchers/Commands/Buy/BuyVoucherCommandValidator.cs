using FluentValidation;

namespace NMCK3.Application.Vouchers.Commands.Buy
{
    internal class BuyVoucherCommandValidator : AbstractValidator<BuyVoucherCommand>
    {
        public BuyVoucherCommandValidator()
        {
            RuleFor(x => x.Count).GreaterThan(0);
            RuleFor(x => x.BuyerId).NotNull().NotEmpty();
        }
    }
}
