using Rahtk.Domain.Features.Product;

namespace Rahtk.Application.Features.category.mappers
{
    public static class WriteCategoryModelToEntity
    {
        public static CategoryEntity ToEntity(this WriteOnlyCategoryModel model)
        {
            return new CategoryEntity
            {
                ArabicName = model.ArabicName,
                EnglishName = model.EnglishName,
            };
        }
    }
}