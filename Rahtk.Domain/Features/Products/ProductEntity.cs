using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.Reminder;

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

		public string Condition { get; set; } = string.Empty;

        public string PriceType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

		public string DeliveryDetails { get; set; } = string.Empty;

		public DateTime CraetedDate { get; set; } = DateTime.Now;

		[ForeignKey("CategoryId")]
		public int CategoryId { get; set; }

		public CategoryEntity? Category { get; set; }

		public int InventoryAmount { get; set; }

        public int PurchasementCount { get; set; }

		[NotMapped]
        public bool IsFavorite { get; set; }

        public ICollection<FavoriteProductUser>? FavoriteProductUsers { get; set; }

        public ICollection<ProductReminder>? Reminders { get; set; }

		public bool Deleted { get; set; }
    }
}