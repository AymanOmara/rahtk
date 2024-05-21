using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.Product;

namespace Rahtk.Domain.Features.Products
{
	public class ProductEntity
	{
		[Key]
		public int Id { get; set; }

		public string ArabicName { get; set; } = string.Empty;

		public string EnglishName { get; set; } = string.Empty;

		public string ArabicDescription { get; set; } = string.Empty;

		public string EnglishDescription { get; set; } = string.Empty;

		public string ImagePath { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public decimal DiscountPercentage { get; set; }

		public DateTime CraetedDate { get; set; } = DateTime.Now;

		[ForeignKey("CategoryId")]
		public int CategoryId { get; set; }

		public CategoryEntity? Category { get; set; }

		public int InventoryAmount { get; set; }

        public int PurchasementCount { get; set; }
    }
}