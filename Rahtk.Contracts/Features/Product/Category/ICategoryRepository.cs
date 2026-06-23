using Microsoft.AspNetCore.Http;
using Rahtk.Domain.Features.Category;

namespace Rahtk.Contracts.Features.Product.Category
{
    public interface ICategoryRepository
    {
        Task<ICollection<CategoryEntity>> GetAllCategories(string email);

        Task<CategoryEntity> CreateCategory(IFormFile file, CategoryEntity category);

        Task<bool> DeleteCategory(int CategoryId);
    }
}