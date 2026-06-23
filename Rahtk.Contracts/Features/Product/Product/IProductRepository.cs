using Microsoft.AspNetCore.Http;
using Rahtk.Domain.Features.Product;

namespace Rahtk.Contracts.Features.Product.Product
{
    public interface IProductRepository
    {
        Task<ICollection<ProductEntity>> GetAllProducts();

        Task<ProductEntity> CreateProduct(ProductEntity productEntity, IFormFile file);

        Task<ProductEntity> AddToFavorite(string email, int productId);

        Task<ProductEntity> RemoveFavorite(string email, int productId);

        Task<ICollection<ProductEntity>> GetFavorites(string email);

        Task<ProductEntity> GetProductDetails(string userEmail, int productId);

        Task<bool> DeleteProduct(int ProductId);
    }
}