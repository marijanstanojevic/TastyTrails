using AutoMapper;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Application.Restaurants.Queries.GetAllRestaurants
{
    public record GetAllRestaurantsResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }

    public class GetAllRestaurantsResponseMapping : Profile
    {
        public GetAllRestaurantsResponseMapping()
        {
            CreateMap<Restaurant, GetAllRestaurantsResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}
