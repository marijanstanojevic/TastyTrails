using MediatR;
using TastyTrails.Application.Orders.Interfaces;

namespace TastyTrails.Application.Orders.Commands.CreateChannelOrder
{
    public class CreateChannelOrderCommandHandler : IRequestHandler<CreateChannelOrderCommand, int>
    {
        private readonly IChannelProviderFactory _channelProviderFactory;
        private readonly IOrderService _orderService;

        public CreateChannelOrderCommandHandler(IChannelProviderFactory channelProviderFactory, IOrderService orderService)
        {
            _channelProviderFactory = channelProviderFactory;
            _orderService = orderService;
        }

        public async Task<int> Handle(CreateChannelOrderCommand request, CancellationToken cancellationToken)
        {
            var provider = _channelProviderFactory.GetProvider(request.Channel);
            var orderDetails = await provider.GetOrderDetailsAsync(request.OrderId, cancellationToken);
            var orderId = await _orderService.CreateAsync(orderDetails, cancellationToken);

            return orderId;
        }
    }
}
