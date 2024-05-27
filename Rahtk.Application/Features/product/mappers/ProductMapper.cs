using System.Globalization;
using Rahtk.Domain.Features.Products;

namespace Rahtk.Application.Features.product.mappers
{
    public static class ProductMapper
    {
        public static ReadProductModel ToModel(this ProductEntity product)
        {
            return new ReadProductModel
            {
                Id = product.Id,
                ArabicDescription = product.ArabicDescription,
                ArabicName = product.ArabicName,
                CategoryId = product.CategoryId,
                CraetedDate = product.CraetedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                InventoryAmount = product.InventoryAmount,
                DiscountPercentage = product.DiscountPercentage,
                EnglishDescription = product.EnglishDescription,
                EnglishName = product.EnglishName,
                PurchasementCount = product.PurchasementCount,
                Price = product.Price,
                ImagePath = product.ImagePath,
                IsFavorite = product.IsFavorite,
                CategoryNameAr = product.Category.ArabicName,
                CategoryNameEn = product.Category.EnglishName,
                Condition = product.Condition,
                Location = product.Location,
                PriceType = product.PriceType,
                DeliveryDetails = product.DeliveryDetails,
            };
        }
    }
}

