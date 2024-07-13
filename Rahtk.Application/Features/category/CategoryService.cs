using Rahtk.Application.Features.category.DTO;
using Rahtk.Application.Features.category.mappers;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Product;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.category
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly LanguageService _langaugeService;
        public CategoryService(IUnitOfWork unitofWork, LanguageService langaugeService)
        {
            _unitofWork = unitofWork;
            _langaugeService = langaugeService;
        }

        public async Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category)
        {
            var categoryEntity = category.ToEntity();

            var result = await _unitofWork.Category.CreateCategory(category.file, categoryEntity);

            return new BaseResponse<CategoryEntity> { data = result, statusCode = 200, success = true };
        }

        public async Task<BaseResponse<bool>> DeleteCategory(int CategoryId)
        {
            var result = await _unitofWork.Category.DeleteCategory(CategoryId);
            return new BaseResponse<bool>
            {
                data = result,
                statusCode = 200,
                success = true,
                message = _langaugeService.Getkey("category_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories(string email)
        {
            var result = await _unitofWork.Category.GetAllCategories(email);
            return new BaseResponse<ICollection<ReadCategoryModel>> { data = result.Select(e => e.ToModel()).ToList(), statusCode = 200, success = true };
        }
    }
}