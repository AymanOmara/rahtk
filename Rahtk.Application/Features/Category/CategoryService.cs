using Rahtk.Application.Features.Category.DTO;
using Rahtk.Application.Features.Category.mappers;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Product;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Category
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
            await _unitofWork.SaveChangesAsync();

            return new BaseResponse<CategoryEntity> { Data = result, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<bool>> DeleteCategory(int CategoryId)
        {
            var result = await _unitofWork.Category.DeleteCategory(CategoryId);
            await _unitofWork.SaveChangesAsync();
            return new BaseResponse<bool>
            {
                Data = result,
                StatusCode = 200,
                Success = true,
                Message = _langaugeService.Getkey("category_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories(string email)
        {
            var result = await _unitofWork.Category.GetAllCategories(email);
            return new BaseResponse<ICollection<ReadCategoryModel>> { Data = result.Select(e => e.ToModel()).ToList(), StatusCode = 200, Success = true };
        }
    }
}