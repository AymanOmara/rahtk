using Rahtk.Application.Features.category.DTO;
using Rahtk.Application.Features.product.mappers;
using Rahtk.Domain.Features.Product;

namespace Rahtk.Application.Features.category.mappers
{
    public static class CategoryMapper
    {
        public static ReadCategoryModel ToModel(this CategoryEntity categoryEntity)
        {
            return new ReadCategoryModel()
            {
                Products = categoryEntity?.Products?.Select(e => e.ToModel()).ToList(),
                ArabicName = categoryEntity.ArabicName,
                EnglishName = categoryEntity.EnglishName,
                Id = categoryEntity.Id,
                ImagePath = categoryEntity.ImagePath
            };
        }
    }
}
