using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.Product.Category;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.User;
using Rahtk.Domain.Features.Category;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class CategoryRepository(RahtkContext context, IFileService fileService, UserManager<RahtkUser> userManager) : ICategoryRepository
    {
        public async Task<CategoryEntity> CreateCategory(IFormFile file, CategoryEntity category)
        {
            var path = await fileService.SaveFileAsync(file);

            category.ImagePath = path;

            await context.Categories.AddAsync(category);

            return category;
        }

        public async Task<bool> DeleteCategory(int CategoryId)
        {
            var category = await context.Categories.FindAsync(CategoryId);
            category.Deleted = true;
            context.Entry(category).State = EntityState.Modified;
            return true;
        }

        public async Task<ICollection<CategoryEntity>> GetAllCategories(string email)
        {
            var categories = await context
                .Categories
                .AsNoTracking()
                .Where(cat => !cat.Deleted)
                .Include(cat => cat.Products)
                .ToListAsync();
            var user = await userManager.FindByEmailAsync(email);
            var favoriteProductIds = await context.FavoriteProductUser
                .AsNoTracking()
                .Where(fpu => fpu.UserId == user.Id)
                .Select(fpu => fpu.ProductId)
                .ToListAsync();
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    product.IsFavorite = favoriteProductIds.Contains(product.Id);
                }
            }
            return categories.Select(cat => new CategoryEntity
            {
                Id = cat.Id,
                ImagePath = cat.ImagePath,
                EnglishName = cat.EnglishName,
                ArabicName = cat.ArabicName,
                Products = cat.Products.Where(pr => !pr.Deleted).ToList()
            }).ToList();
        }
    }
}
