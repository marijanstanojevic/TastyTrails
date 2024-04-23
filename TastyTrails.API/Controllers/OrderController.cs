using MediatR;
using Microsoft.AspNetCore.Mvc;
using TastyTrails.Application.Orders.Commands.CreateDirectOrder;
using TastyTrails.Application.Orders.Queries.GetOrderStatus;

namespace TastyTrails.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateDirectOrderCommand request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpGet("{orderId}/status")]
        public async Task<IActionResult> GetOrderStatusAsync([FromRoute] int orderId)
        {
            var query = new GetOrderStatusQuery(orderId);
            return Ok(await _mediator.Send(query));
        }
    }
    }
