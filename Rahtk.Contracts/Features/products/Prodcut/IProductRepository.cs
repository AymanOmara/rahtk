using Microsoft.AspNetCore.Http;
using Rahtk.Domain.Features.Products;

namespace Rahtk.Contracts.Features.products.Prodcut
{
	public interface IProductRepository
	{
        Task<ICollection<ProductEntity>> GetAllProducts();

        Task<ProductEntity> CreateProduct(ProductEntity productEntity,IFormFile file);
    }
}

