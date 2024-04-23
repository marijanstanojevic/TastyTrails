using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Application.Orders.Queries.GetOrderStatus
{
    public record GetOrderStatusQuery : IRequest<GetOrderStatusResponse>
    {
        public int Id { get; init; }

        public GetOrderStatusQuery(int orderId)
        {
            Id = orderId;
        }
    }

    public class GetOrderStatusQueryValidator : AbstractValidator<GetOrderStatusQuery>
    {
        private readonly ITastyTrailsDbContext _dbContext;
        public GetOrderStatusQueryValidator(ITastyTrailsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id)
                .MustAsync(OrderExistsAsync).WithMessage("Order with id {PropertyValue} doesn't exists.");
        }

        private async Task<bool> OrderExistsAsync(int orderId, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders.AnyAsync(x => x.Id == orderId, cancellationToken);
        }
    }
}
