using Rahtk.Application.Features.product.DTO;
using Rahtk.Application.Features.product.mappers;
using Rahtk.Domain.Features.Products;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.product
{
    public interface IProductService
    {
        Task<BaseResponse<ProductEntity>> CreateProduct(CreateProductModel model);

        Task<BaseResponse<ICollection<ProductEntity>>> GetAllProducts();

        Task<BaseResponse<ReadProductModel>> AddToFavorite(string userEmail, int productId);

        Task<BaseResponse<ReadProductModel>> RemoveFavorite(string userEmail, int productId);

        Task<BaseResponse<ICollection<ProductEntity>>> GetFavorites(string userEmail);

        Task<BaseResponse<ReadProductModel>> ProductDetails(string userEmail, int productId);
    }
}