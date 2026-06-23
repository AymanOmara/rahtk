using Rahtk.Application.Features.Category.DTO;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.Category;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Category
{
	public interface ICategoryService
	{
        Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category);

        Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories(string userId);

        Task<BaseResponse<bool>> DeleteCategory(int CategoryId);
    }
}