using Rahtk.Application.Features.product.mappers;

namespace Rahtk.Application.Features.category.DTO
{
	public class ReadCategoryModel
	{
        public int Id { get; set; }

        public string ArabicName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public ICollection<ReadProductModel>? Products { get; set; }
    }
}