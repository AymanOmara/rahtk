using Rahtk.Application.Features.Product.DTO;

namespace Rahtk.Application.Features.Category.DTO
{
	public record ReadCategoryModel(
        int Id,
        string ArabicName,
        string EnglishName,
        string ImagePath,
        ICollection<ReadProductModel>? Products
    );
}