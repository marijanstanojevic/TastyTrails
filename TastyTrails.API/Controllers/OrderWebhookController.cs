using MediatR;
using Microsoft.AspNetCore.Mvc;
using TastyTrails.Application.Common.Enums;
using TastyTrails.Application.Orders.Commands.CreateChannelOrder;

namespace TastyTrails.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderWebhookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderWebhookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("wolt/order-created/{orderId}")]
        public async Task<IActionResult> WoltOrderWebhookAsync([FromRoute] int orderId)
        {
            var command = new CreateChannelOrderCommand
            {
                OrderId = orderId.ToString(),
                Channel = ExternalChannel.Wolt
            };

            return Ok(await  _mediator.Send(command));
        }
    }
}
