using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TastyTrails.Application.Common.Interfaces;

namespace TastyTrails.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<GetAllRestaurantsResponse>>
    {
        private readonly ITastyTrailsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllRestaurantsQueryHandler(ITastyTrailsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllRestaurantsResponse>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _dbContext
                .Restaurants
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<GetAllRestaurantsResponse>>(restaurants);
        }
    }
}
