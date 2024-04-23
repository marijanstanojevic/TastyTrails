using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using TastyTrails.Application.Restaurants.Queries.GetAllRestaurants;
using TastyTrails.Application.Restaurants.Queries.GetRestaurantMenus;
using TastyTrails.Domain.Entities;

namespace TastyTrails.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [OutputCache(Duration = 300)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(result);
        }

        [HttpGet("{restaurantId}/menus")]
        [OutputCache(Duration = 300, VaryByRouteValueNames = [nameof(restaurantId)])]
        public async Task<IActionResult> GetRestaurantMenusAsync([FromRoute] int restaurantId)
        {
            var request = new GetRestaurantMenusQuery(restaurantId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
