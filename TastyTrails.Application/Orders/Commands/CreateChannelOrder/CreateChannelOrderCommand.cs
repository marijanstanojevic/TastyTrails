using FluentValidation;
using MediatR;
using TastyTrails.Application.Common.Enums;

namespace TastyTrails.Application.Orders.Commands.CreateChannelOrder
{
    public class CreateChannelOrderCommand : IRequest<int>
    {
        public string OrderId { get; set; }
        public ExternalChannel Channel { get; set; }
    }

    public class CreateChannelOrderCommandValidator : AbstractValidator<CreateChannelOrderCommand>
    {
        public CreateChannelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty");
        }
    }
}
