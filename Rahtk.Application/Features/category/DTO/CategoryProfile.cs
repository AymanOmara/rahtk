using AutoMapper;
using Rahtk.Domain.Features.Product;

namespace Rahtk.Application.Features.category.DTO
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<WriteOnlyCategoryModel, CategoryEntity>();
        }
    }
}

