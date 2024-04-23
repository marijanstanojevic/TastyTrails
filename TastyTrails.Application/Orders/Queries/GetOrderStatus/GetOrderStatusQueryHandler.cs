using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Application.Orders.Queries.GetOrderStatus
{
    public class GetOrderStatusQueryHandler : IRequestHandler<GetOrderStatusQuery, GetOrderStatusResponse>
    {
        private readonly ITastyTrailsDbContext _dbContext;

        public GetOrderStatusQueryHandler(ITastyTrailsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetOrderStatusResponse> Handle(GetOrderStatusQuery request, CancellationToken cancellationToken)
        {
            var orderStatus = await _dbContext.Orders
                .AsNoTracking()
                .Where(o => o.Id == request.Id)
                .Select(o => o.Status)
                .FirstAsync(cancellationToken);

            return new GetOrderStatusResponse
            {
                Status = orderStatus.ToString()
            };
        }
    }
}
