using AutoMapper;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Loans;

namespace Engineer.Mapping.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        }
    }
}