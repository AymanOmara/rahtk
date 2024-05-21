using Microsoft.AspNetCore.Http;

namespace Rahtk.Application.Features.product.DTO
{
	public class CreateProductModel
	{
        public string ArabicName { get; set; } = string.Empty;

        public string EnglishName { get; set; } = string.Empty;

        public string ArabicDescription { get; set; } = string.Empty;

        public string EnglishDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal DiscountPercentage { get; set; }

        public int CategoryId { get; set; }

        public int InventoryAmount { get; set; }

        public  IFormFile file { get; set; }

        public int PurchasementCount { get; set; }
    }
}

