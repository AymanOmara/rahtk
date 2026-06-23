using Rahtk.Domain.Features.Category;
using Rahtk.Application.Features.Category.DTO;

namespace Rahtk.Application.Features.Category.mappers
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