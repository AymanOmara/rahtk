using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rahtk.Contracts.Common;
using Rahtk.Contracts.Features.products.Prodcut;
using Rahtk.Domain.Features.Products;
using Rahtk.Infrastructure.EF.Contexts;

namespace Rahtk.Infrastructure.EF.Repositories
{
	public class ProductRepository: IProductRepository
    {
        private readonly RahtkContext _context;

        private readonly IFileService _fileService;

		public ProductRepository(RahtkContext context, IFileService fileService)
		{
            _context = context;
            _fileService = fileService;
        }



        public async Task<ICollection<ProductEntity>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

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
    }
}

