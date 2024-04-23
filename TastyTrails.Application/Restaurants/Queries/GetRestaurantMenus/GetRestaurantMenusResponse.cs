using AutoMapper;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Application.Restaurants.Queries.GetRestaurantMenus
{
    public record GetRestaurantMenusResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public IEnumerable<RestaurantMenuItem> Items { get; init; }
    }

    public record RestaurantMenuItem
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
    }

    public class GetRestaurantMenusResponseMapping : Profile
    {
        public GetRestaurantMenusResponseMapping()
        {
            CreateMap<MenuItem, RestaurantMenuItem>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(s => s.Price));

            CreateMap<Menu, GetRestaurantMenusResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(x => x.Items, opt => opt.MapFrom(s => s.Items));
        }
    }
}
