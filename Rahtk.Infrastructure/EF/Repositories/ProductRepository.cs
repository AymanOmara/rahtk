using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.products.Prodcut;
using Rahtk.Domain.Features.Products;
using Rahtk.Domain.Features.User;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RahtkContext _context;

        private readonly IFileService _fileService;
        private readonly UserManager<RahtkUser> _userManager;
        public ProductRepository(RahtkContext context, IFileService fileService, UserManager<RahtkUser> userManager)
        {
            _context = context;

            _fileService = fileService;

            _userManager = userManager;
        }



        public async Task<ICollection<ProductEntity>> GetAllProducts()
        {
            var products = await _context.Products.Where(pr=>!pr.Deleted).ToListAsync();

            return products;
        }

        public async Task<ProductEntity> CreateProduct(ProductEntity product, IFormFile file)
        {
            var productPath = await _fileService.SaveFileAsync(file);

            product.ImagePath = productPath;

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<ProductEntity> AddToFavorite(string email, int productId)
        {

            var product = await _context.Products.Include(pro => pro.FavoriteProductUsers).Include(cat=>cat.Category).FirstOrDefaultAsync(pro => pro.Id == productId);
            var user = await _userManager.FindByEmailAsync(email);

            product.FavoriteProductUsers.Add(new FavoriteProductUser() { UserId = user.Id, ProductId = productId });
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            product.IsFavorite = true;
            return product;
        }

        public async Task<ProductEntity?> RemoveFavorite(string email, int productId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var favorite = await _context.FavoriteProductUser.Include(p => p.Product).ThenInclude(cat=>cat.Category).Include(p => p.User).FirstOrDefaultAsync(prod => prod.UserId == user.Id && prod.ProductId == productId);
            _context.Entry(favorite).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            favorite.Product.IsFavorite = false;
            return favorite?.Product;
        }

        public async Task<ICollection<ProductEntity>> GetFavorites(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var favoriteProductIds = await _context.FavoriteProductUser
            .Where(fpu => fpu.UserId == user.Id)
            .Select(fpu => fpu.ProductId)
            .ToListAsync();
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.IsFavorite = favoriteProductIds.Contains(product.Id);
            }

            return products.Where(pro => pro.IsFavorite).ToList();
        }

        public async Task<ProductEntity> GetProductDetails(string email, int productId)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var favoriteProductIds = await _context.FavoriteProductUser
            .Where(fpu => fpu.UserId == user.Id)
            .Select(fpu => fpu.ProductId)
            .ToListAsync();

            var product = await _context.Products.Include(pro=> pro.Category).FirstOrDefaultAsync(prod => productId == prod.Id);
            if (favoriteProductIds.Contains(product.Id))
            {
                product.IsFavorite = true;
            }
            return product;
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            product.Deleted = true;
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

