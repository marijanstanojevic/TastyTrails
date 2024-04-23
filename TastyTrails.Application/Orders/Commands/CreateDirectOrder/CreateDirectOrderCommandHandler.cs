using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Application.Orders.Interfaces;
using TastyTrails.Application.Orders.Models;

namespace TastyTrails.Application.Orders.Commands.CreateDirectOrder
{
    public class CreateDirectOrderCommandHandler : IRequestHandler<CreateDirectOrderCommand, CreateDirectOrderResponse>
    {
        private readonly IOrderService _orderService;
        private readonly ITastyTrailsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectOrderCommandHandler(IOrderService orderService, ITastyTrailsDbContext dbContext, IMapper mapper)
        {
            _orderService = orderService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CreateDirectOrderResponse> Handle(CreateDirectOrderCommand request, CancellationToken cancellationToken)
        {
            var menuItemIds = request.Items
                .Select(x => x.ItemId)
                .Distinct()
                .ToList();

            var menuItems = _dbContext.MenuItems
                .Where(x => menuItemIds.Contains(x.Id))
                .ToList();

            var orderDetailItems = request.Items
                .Join(menuItems,
                    r => r.ItemId,
                    o => o.Id, (r, o) => new OrderDetailItem
                    {
                        Amount = r.Amount,
                        MenuItem = o
                    })
                .ToArray();

            var orderDetails = new DirectOrderDetails()
            {
                Items = orderDetailItems
            };

            var orderId = await _orderService.CreateAsync(orderDetails, cancellationToken);
            var order = await _dbContext.Orders.FirstAsync(x => x.Id == orderId, cancellationToken);

            return _mapper.Map<CreateDirectOrderResponse>(order);
        }
    }
}
