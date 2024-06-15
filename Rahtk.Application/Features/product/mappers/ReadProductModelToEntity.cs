using Rahtk.Domain.Features.Products;

namespace Rahtk.Application.Features.product.mappers
{
	public static class ReadProductModelToEntity
	{
		public static ProductEntity ToEntity(this ReadProductModel model) {
            return new ProductEntity
            {
                Id = model.Id,
                ArabicDescription = model.ArabicDescription,
                ArabicName = model.ArabicName,
                InventoryAmount = model.InventoryAmount,
                CategoryId = model.CategoryId,
                Condition = model.Condition,
                DeliveryDetails = model.DeliveryDetails,
                DiscountPercentage = model.DiscountPercentage,
                EnglishDescription = model.EnglishDescription,
                EnglishName = model.EnglishName,
                Location = model.Location,
                PriceType = model.PriceType,
                Price = model.Price,
                PurchasementCount = model.PurchasementCount,
            };
        }
	}
}