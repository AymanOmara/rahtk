using Rahtk.Domain.Features.Product;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.category
{
	public interface ICategoryApplication
	{
        Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category);

        Task<BaseResponse<ICollection<CategoryEntity>>> GetAllCategories();
    }
}

