using Microsoft.AspNetCore.Http;
using Rahtk.Domain.Features.Products;

namespace Rahtk.Contracts.Features.products.Prodcut
{
    public interface IProductRepository
    {
        Task<ICollection<ProductEntity>> GetAllProducts();

        Task<ProductEntity> CreateProduct(ProductEntity productEntity, IFormFile file);

        Task<ProductEntity> AddToFavorite(string email, int productId);

        Task<ProductEntity> RemoveFavorite(string email, int productId);

        Task<ICollection<ProductEntity>> GetFavorites(string email);

        Task<ProductEntity> GetProductDetails(string userEmail, int productId);
    }
}

