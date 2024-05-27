using Rahtk.Application.Features.category.DTO;
using Rahtk.Domain.Features.Product;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.category
{
	public interface ICategoryService
	{
        Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category);

        Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories(string userId);
    }
}