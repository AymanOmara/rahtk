using Rahtk.Application.Features.Category.DTO;
using Rahtk.Application.Features.Product.DTO;
using Rahtk.Application.Features.Product.mappers;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.Category;

namespace Rahtk.Application.Features.Category.mappers
{
    public static class CategoryMapper
    {
        public static ReadCategoryModel ToModel(this CategoryEntity categoryEntity)
        {
            return new ReadCategoryModel(
                categoryEntity.Id,
                categoryEntity.ArabicName,
                categoryEntity.EnglishName,
                categoryEntity.ImagePath,
                categoryEntity?.Products?.Select(e => e.ToModel()).ToList()
            );
        }
    }
}
