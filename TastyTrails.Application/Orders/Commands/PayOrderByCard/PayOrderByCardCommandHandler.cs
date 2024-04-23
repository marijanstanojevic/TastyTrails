using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;
using TastyTrails.Domain.Enums;

namespace TastyTrails.Application.Orders.Commands.PayOrderByCard
{
    public class PayOrderByCardCommandHandler : IRequestHandler<PayOrderByCardCommand>
    {
        private readonly ITastyTrailsDbContext _dbContext;

        public PayOrderByCardCommandHandler(ITastyTrailsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(PayOrderByCardCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstAsync(x => x.Id == request.OrderId, cancellationToken);
            order.PaymentStatus = PaymentStatus.Paid;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
