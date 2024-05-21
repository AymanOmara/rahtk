namespace Rahtk.Application.Features.product.mappers
{
	public class ReadProductModel
	{
        public int Id { get; set; }

        public string ArabicName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

        public string ArabicDescription { get; set; } = string.Empty;

        public string EnglishDescription { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal DiscountPercentage { get; set; }

        public string CraetedDate { get; set; }

        public int CategoryId { get; set; }

        public int InventoryAmount { get; set; }

        public int PurchasementCount { get; set; }
    }
}

