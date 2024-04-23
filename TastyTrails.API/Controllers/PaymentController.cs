using MediatR;
using Microsoft.AspNetCore.Mvc;
using TastyTrails.Application.Orders.Commands.PayOrderByCard;

namespace TastyTrails.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("card")]
        public async Task<IActionResult> PayByCardAsync([FromBody] PayOrderByCardCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
