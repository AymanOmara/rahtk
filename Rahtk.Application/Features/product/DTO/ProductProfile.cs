using AutoMapper;
using Rahtk.Domain.Features.Products;

namespace Rahtk.Application.Features.product.DTO
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductModel, ProductEntity>().ReverseMap();
        }
    }
}

