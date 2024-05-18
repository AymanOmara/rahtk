namespace Rahtk.Domain.Features.Product
{
	public class CategoryEntity
	{
		public int Id { get; set; }

		public string ArabicName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

		public string ImagePath { get; set; } = string.Empty;
	}
}