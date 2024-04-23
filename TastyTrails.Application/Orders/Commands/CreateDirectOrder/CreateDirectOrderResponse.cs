using AutoMapper;
using TastyTrails.Domain.Entities;

namespace TastyTrails.Application.Orders.Commands.CreateDirectOrder
{
    public record CreateDirectOrderResponse
    {
        public int Id { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; }
    }

    public class CreateDirectOrderResponseMapping : Profile
    {
        public CreateDirectOrderResponseMapping()
        {
            CreateMap<Order, CreateDirectOrderResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.TotalPrice, opt => opt.MapFrom(s => s.TotalPrice))
                .ForMember(x => x.Status, opt => opt.MapFrom(s => s.Status));
        }
    }
}
