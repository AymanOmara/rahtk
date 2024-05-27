using Microsoft.AspNetCore.Http;
using Rahtk.Domain.Features.Product;

namespace Rahtk.Contracts.Features
{
    public interface ICategoryRepository
    {
        Task<ICollection<CategoryEntity>> GetAllCategories(string email);

        Task<CategoryEntity> CreateCategory(IFormFile file, CategoryEntity category);
    }
}