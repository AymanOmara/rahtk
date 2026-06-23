using Rahtk.Application.Features.Category.DTO;
using Rahtk.Application.Features.Product.mappers;
using Rahtk.Domain.Features.Product;

namespace Rahtk.Application.Features.Category.mappers
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
