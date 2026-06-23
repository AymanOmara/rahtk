using Rahtk.Domain.Features.Product;

namespace Rahtk.Domain.Features.Category
{
	public class CategoryEntity
	{
		public int Id { get; set; }

		public string ArabicName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

		public string ImagePath { get; set; } = string.Empty;

		public ICollection<ProductEntity>? Products { get; set; }

		public bool Deleted { get; set; }
	}
}