using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features;
using Rahtk.Domain.Features.Product;
using Rahtk.Infrastructure.EF.Contexts;
using Rahtk.Shared.Localization;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RahtkContext _context;

        private readonly LanguageService _languageService;

        private readonly IFileService _fileService;


        public CategoryRepository(RahtkContext context, LanguageService languageService, IFileService fileService)
        {
            _context = context;

            _languageService = languageService;

            _fileService = fileService;

        }

        public async Task<CategoryEntity> CreateCategory(IFormFile file, CategoryEntity category)
        {
            var path = await _fileService.SaveFileAsync(file);

            category.ImagePath = path;

            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<ICollection<CategoryEntity>> GetAllCategories()
        {
            var categories = await _context.Categories.Include(cat=> cat.Products).ToListAsync();

            return categories;
        }

    }
}

