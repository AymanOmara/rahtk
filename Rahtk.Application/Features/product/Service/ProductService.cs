using Rahtk.Application.Features.product.DTO;
using Rahtk.Application.Features.product.mappers;
using Rahtk.Contracts.Common;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.product.Service
{
    public class ProductService : IProductService
	{
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly LanguageService _languageService;
		public ProductService(IUnitOfWork unitOfWork, LanguageService languageService)
		{
            _unitOfWork = unitOfWork;

            _languageService = languageService;
		}

        public async Task<BaseResponse<ReadProductModel>> AddToFavorite(string email,int productId)
        {
            var result = await _unitOfWork.Product.AddToFavorite(email,productId);
            return new BaseResponse<ReadProductModel> { data = result.ToModel(), statusCode = 200, message = "", success = true };
        }

        public async Task<BaseResponse<ReadProductModel>> CreateProduct(CreateProductModel model)
        {
            var entity = model.ToEntity();

            var result = await _unitOfWork.Product.CreateProduct(entity, model.file);

            return new BaseResponse<ReadProductModel> { data = result.ToModel(), statusCode = 200, message = _languageService.Getkey("product_created_successfully").Value, success = true };
        }

        public async Task<BaseResponse<bool>> DeleteProduct(int ProductId)
        {
            var result = await _unitOfWork.Product.DeleteProduct(ProductId);
            return new BaseResponse<bool>
            {
                data = result,
                statusCode = 200,
                success = true,
                message = _languageService.Getkey("product_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadProductModel>>> GetAllProducts()
        {
            var result = await _unitOfWork.Product.GetAllProducts();
            return new BaseResponse<ICollection<ReadProductModel>> { data = result.Select(e=>e.ToModel()).ToList(), statusCode = 200, success = true };
        }

        public async Task<BaseResponse<ICollection<ReadProductModel>>> GetFavorites(string email)
        {
            var result = await _unitOfWork.Product.GetFavorites(email);
            return new BaseResponse<ICollection<ReadProductModel>> { data = result.Select(pr=> pr.ToModel()).ToList(), statusCode = 200, success = true };
        }

        public async Task<BaseResponse<ReadProductModel>> ProductDetails(string userEmail, int productId)
        {
            var result = await _unitOfWork.Product.GetProductDetails(userEmail,productId);
            return new BaseResponse<ReadProductModel> { data = result.ToModel(), statusCode = 200, message = "", success = true };
        }

        public async Task<BaseResponse<ReadProductModel>> RemoveFavorite(string email,int productId)
        {
            var result = await _unitOfWork.Product.RemoveFavorite(email, productId);
            return new BaseResponse<ReadProductModel> { data = result.ToModel(), statusCode = 200, message = "", success = true };
        }
    }
}

