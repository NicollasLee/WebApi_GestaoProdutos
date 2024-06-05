using AutoMapper;
using GestaoProdutos.Domain.Dto;
using GestaoProdutos.Domain.Entidades;

namespace GestaoProdutos.Domain.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }

}
