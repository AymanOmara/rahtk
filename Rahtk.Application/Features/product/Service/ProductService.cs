using AutoMapper;
using Rahtk.Application.Features.product.DTO;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Products;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.product.Service
{
    public class ProductService : IProductService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LanguageService _languageService;
		public ProductService(IUnitOfWork unitOfWork, IMapper mapper, LanguageService languageService)
		{
            _unitOfWork = unitOfWork;

            _mapper = mapper;

            _languageService = languageService;
		}

        public async Task<BaseResponse<ProductEntity>> CreateProduct(CreateProductModel model)
        {
            var entity = _mapper.Map<ProductEntity>(model);

            var result = await _unitOfWork.Product.CreateProduct(entity, model.file);

            return new BaseResponse<ProductEntity> { data = result, statusCode = 200, message = _languageService.Getkey("product_created_successfully").Value, success = true };
        }

        public async Task<BaseResponse<ICollection<ProductEntity>>> GetAllProducts()
        {
            var result = await _unitOfWork.Product.GetAllProducts();
            return new BaseResponse<ICollection<ProductEntity>> { data = result, statusCode = 200, success = true };
        }
    }
}

