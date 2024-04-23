using MediatR;

namespace TastyTrails.Application.Restaurants.Queries.GetAllRestaurants
{
    public record GetAllRestaurantsQuery : IRequest<IEnumerable<GetAllRestaurantsResponse>> { }
}
