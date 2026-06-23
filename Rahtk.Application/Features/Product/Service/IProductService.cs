using Rahtk.Application.Features.Product.DTO;
using Rahtk.Application.Features.Product.mappers;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Product
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