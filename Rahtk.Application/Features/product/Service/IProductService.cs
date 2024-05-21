using Rahtk.Application.Features.product.DTO;
using Rahtk.Domain.Features.Products;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.product
{
    public interface IProductService
    {
        Task<BaseResponse<ProductEntity>> CreateProduct(CreateProductModel model);

        Task<BaseResponse<ICollection<ProductEntity>>> GetAllProducts();
    }
}

