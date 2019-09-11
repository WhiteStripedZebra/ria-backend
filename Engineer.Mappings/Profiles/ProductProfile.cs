using AutoMapper;
using Engineer.Domain.Entities;
using Engineer.Domain.Models.Loans;

namespace Engineer.Mapping.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }      
    }
}