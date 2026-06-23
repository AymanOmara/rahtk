using Rahtk.Application.Features.Product.DTO;
using Rahtk.Application.Features.Product.mappers;
using Rahtk.Contracts.Common;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Product.Service
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
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<ReadProductModel> { Data = result.ToModel(), StatusCode = 200, Message = "", Success = true };
        }

        public async Task<BaseResponse<ReadProductModel>> CreateProduct(CreateProductModel model)
        {
            var entity = model.ToEntity();

            var result = await _unitOfWork.Product.CreateProduct(entity, model.file);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<ReadProductModel> { Data = result.ToModel(), StatusCode = 200, Message = _languageService.Getkey("product_created_successfully").Value, Success = true };
        }

        public async Task<BaseResponse<bool>> DeleteProduct(int ProductId)
        {
            var result = await _unitOfWork.Product.DeleteProduct(ProductId);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<bool>
            {
                Data = result,
                StatusCode = 200,
                Success = true,
                Message = _languageService.Getkey("product_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadProductModel>>> GetAllProducts()
        {
            var result = await _unitOfWork.Product.GetAllProducts();
            return new BaseResponse<ICollection<ReadProductModel>> { Data = result.Select(e=>e.ToModel()).ToList(), StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<ICollection<ReadProductModel>>> GetFavorites(string email)
        {
            var result = await _unitOfWork.Product.GetFavorites(email);
            return new BaseResponse<ICollection<ReadProductModel>> { Data = result.Select(pr=> pr.ToModel()).ToList(), StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<ReadProductModel>> ProductDetails(string userEmail, int productId)
        {
            var result = await _unitOfWork.Product.GetProductDetails(userEmail,productId);
            return new BaseResponse<ReadProductModel> { Data = result.ToModel(), StatusCode = 200, Message = "", Success = true };
        }

        public async Task<BaseResponse<ReadProductModel>> RemoveFavorite(string email,int productId)
        {
            var result = await _unitOfWork.Product.RemoveFavorite(email, productId);
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponse<ReadProductModel> { Data = result.ToModel(), StatusCode = 200, Message = "", Success = true };
        }
    }
}

