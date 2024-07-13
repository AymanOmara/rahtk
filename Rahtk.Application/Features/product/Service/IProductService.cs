using Rahtk.Application.Features.product.DTO;
using Rahtk.Application.Features.product.mappers;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.product
{
    public interface IProductService
    {
        Task<BaseResponse<ReadProductModel>> CreateProduct(CreateProductModel model);

        Task<BaseResponse<ICollection<ReadProductModel>>> GetAllProducts();

        Task<BaseResponse<ReadProductModel>> AddToFavorite(string userEmail, int productId);

        Task<BaseResponse<ReadProductModel>> RemoveFavorite(string userEmail, int productId);

        Task<BaseResponse<ICollection<ReadProductModel>>> GetFavorites(string userEmail);

        Task<BaseResponse<ReadProductModel>> ProductDetails(string userEmail, int productId);

        Task<BaseResponse<bool>> DeleteProduct(int ProductId);
    }
}