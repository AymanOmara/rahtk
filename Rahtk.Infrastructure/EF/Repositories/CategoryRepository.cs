using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RahtkContext _context;

        private readonly LanguageService _languageService;

        private readonly IFileService _fileService;

        private readonly UserManager<RahtkUser> _userManager;

        private readonly INotificationSender _sender;

        public CategoryRepository(RahtkContext context, LanguageService languageService, IFileService fileService, UserManager<RahtkUser> userManager, INotificationSender sender)
        {
            _context = context;

            _languageService = languageService;

            _fileService = fileService;

            _userManager = userManager;

            _sender = sender;

        }

        public async Task<CategoryEntity> CreateCategory(IFormFile file, CategoryEntity category)
        {
            var path = await _fileService.SaveFileAsync(file);

            category.ImagePath = path;

            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategory(int CategoryId)
        {
            var category = await _context.Categories.FindAsync(CategoryId);
            category.Deleted = true;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<CategoryEntity>> GetAllCategories(string email)
        {
            var categories = await _context
                .Categories
                .Where(cat => !cat.Deleted)
                .Include(cat => cat.Products)
                .ToListAsync();
            var user = await _userManager.FindByEmailAsync(email);
            var favoriteProductIds = await _context.FavoriteProductUser
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

