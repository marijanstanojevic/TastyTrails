using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Application.Restaurants.Queries.GetRestaurantMenus
{
    public record GetRestaurantMenusQuery : IRequest<IEnumerable<GetRestaurantMenusResponse>>
    {
        public int RestaurantId { get; }

        public GetRestaurantMenusQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
   
    public class GetRestaurantMenusQueryValidator : AbstractValidator<GetRestaurantMenusQuery>
    {
        private readonly ITastyTrailsDbContext _dbContext;
        public GetRestaurantMenusQueryValidator(ITastyTrailsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleLevelCascadeMode = CascadeMode.Stop;
                
            RuleFor(x => x.RestaurantId)
                .GreaterThan(0).WithMessage("Value '{PropertyValue}' for '{PropertyName}' is not valid.")
                .MustAsync(RestaurantExistsAsync).WithMessage("Restaurant not found.");
        }

        private async Task<bool> RestaurantExistsAsync(int restaurantId, CancellationToken cancellationToken)
        {
            return await _dbContext.Restaurants.AnyAsync(x => x.Id == restaurantId, cancellationToken);
        }
    }
}
