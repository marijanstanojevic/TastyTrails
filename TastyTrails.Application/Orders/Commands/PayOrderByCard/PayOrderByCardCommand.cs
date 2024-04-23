using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Domain.Enums;

namespace TastyTrails.Application.Orders.Commands.PayOrderByCard
{
    public record PayOrderByCardCommand : IRequest
    {
        public int OrderId { get; init; }
        public string CardNumber { get; init; }
        public string HolderName { get; init; }
        public int ExpiryMonth { get; init; }
        public int ExpiryYear { get; init;}
        public string CVV { get; init; }
    }

    public class PayOrderByCardCommandValidator : AbstractValidator<PayOrderByCardCommand>
    {
        private readonly IClock _clock;
        private readonly ITastyTrailsDbContext _dbContext;

        public PayOrderByCardCommandValidator(IClock clock, ITastyTrailsDbContext dbContext)
        {
            _clock = clock;
            _dbContext = dbContext;

            RuleLevelCascadeMode = CascadeMode.Continue;

            RuleFor(x => x.OrderId)
                .MustAsync(UnpaidOrderExistsAsync).WithMessage("The order does not exist or has already been paid.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .CreditCard().WithMessage("Invalid card number.");

            RuleFor(x => x.HolderName)
                .NotEmpty().WithMessage("Card holder name is required.")
                .Matches("^[a-zA-Z ]+$").WithMessage("Card holder name must contain only letters and spaces.");

            RuleFor(x => x.ExpiryMonth)
                .InclusiveBetween(1, 12).WithMessage("Expiry month must be between 01 and 12.");

            RuleFor(x => x.ExpiryYear)
                .Must((cmd, _) => BeAValidYearAndMonth(cmd.ExpiryYear, cmd.ExpiryMonth))
                .WithMessage("Expiry year is not valid or expired.");

            RuleFor(x => x.CVV)
                .NotEmpty().WithMessage("CVV is required")
                .Matches("^[0-9]+$").WithMessage("CVV must contain only digits.")
                .Length(3, 4).WithMessage("CVV must be 3 or 4 digits long.");

        }

        private async Task<bool> UnpaidOrderExistsAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders.AnyAsync(x => x.Id == orderId && x.PaymentStatus == PaymentStatus.Unpaid, cancellationToken);
        }
        

        private bool BeAValidYearAndMonth(int year, int month)
        {
            var now = _clock.UtcNow;

            if(year > now.Year)
            {
                return true;
            }

            return year == now.Year && month >= now.Month;
        }
    }
}
