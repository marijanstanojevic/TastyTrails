using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Application.Orders.Commands.CreateDirectOrder
{
    public record CreateDirectOrderCommand : IRequest<CreateDirectOrderResponse>
    {
        public List<CreateDirectOrderItem> Items { get; init; } = new();
    }

    public record CreateDirectOrderItem
    {
        public int ItemId { get; init; }
        public int Amount { get; init; }
    }

    public class CreateDirectOrderCommandValidator : AbstractValidator<CreateDirectOrderCommand>
    {
        private readonly ITastyTrailsDbContext _dbContext;

        public CreateDirectOrderCommandValidator(ITastyTrailsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleForEach(x => x.Items)
                .Must(x => x.Amount > 0).WithMessage("Amount must be greater than 0.")
                .Must(x => x.ItemId > 0).WithMessage("Invalid ItemId. {PropertyName}");

            RuleFor(x => x.Items)
                .Must(x => x.Count > 0).WithMessage("At least one item in order is required.")
                .MustAsync(AllItemsExistsInSameRestaurantAsync).WithMessage("Some item doesn't exist or do not belong to the same restaurant.");
        }

        private async Task<bool> AllItemsExistsInSameRestaurantAsync(List<CreateDirectOrderItem> items, CancellationToken cancellationToken)
        {
            if (items == null || items.Count == 0)
            {
                return false;
            }

            var itemIds = items
                .Select(x => x.ItemId)
                .Distinct()
                .ToList();

            var restaurantIds = await _dbContext.Menus
                .AsNoTracking()
                .Where(menu => menu.Items.Any(item => itemIds.Contains(item.Id)))
                .Select(menu => menu.Restaurant.Id)
                .Distinct()
                .ToListAsync(cancellationToken);

            return restaurantIds.Count == 1;
        }
    }
}
