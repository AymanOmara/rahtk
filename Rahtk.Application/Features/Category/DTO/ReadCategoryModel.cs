using Rahtk.Application.Features.Product.mappers;

namespace Rahtk.Application.Features.Category.DTO
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