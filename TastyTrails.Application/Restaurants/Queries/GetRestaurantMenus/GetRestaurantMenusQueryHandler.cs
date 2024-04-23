using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Application.Restaurants.Queries.GetRestaurantMenus
{
    public class GetRestaurantMenusQueryHandler : IRequestHandler<GetRestaurantMenusQuery, IEnumerable<GetRestaurantMenusResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ITastyTrailsDbContext _dbContext;

        public GetRestaurantMenusQueryHandler(IMapper mapper, ITastyTrailsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetRestaurantMenusResponse>> Handle(GetRestaurantMenusQuery request, CancellationToken cancellationToken)
        {
            var menus = await _dbContext
                .Menus
                .AsNoTracking()
                .Where(r => r.Restaurant.Id == request.RestaurantId)
                .Include(r => r.Items)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<GetRestaurantMenusResponse>>(menus);
        }
    }
}
