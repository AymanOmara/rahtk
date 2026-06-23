using Rahtk.Application.Features.Category.DTO;
using Rahtk.Application.Features.Category.mappers;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Product;
using Rahtk.Domain.Features.Category;
using Rahtk.Shared.Localization;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.Category
{
    public class CategoryService(IUnitOfWork unitOfWork, LanguageService languageService) : ICategoryService
    {
        public async Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category)
        {
            var categoryEntity = category.ToEntity();

            var result = await unitOfWork.Category.CreateCategory(category.file, categoryEntity);
            await unitOfWork.SaveChangesAsync();

            return new BaseResponse<CategoryEntity> { Data = result, StatusCode = 200, Success = true };
        }

        public async Task<BaseResponse<bool>> DeleteCategory(int CategoryId)
        {
            var result = await unitOfWork.Category.DeleteCategory(CategoryId);
            await unitOfWork.SaveChangesAsync();
            return new BaseResponse<bool>
            {
                Data = result,
                StatusCode = 200,
                Success = true,
                Message = languageService.GetKey("category_deleted_successfully").Value
            };
        }

        public async Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories(string email)
        {
            var result = await unitOfWork.Category.GetAllCategories(email);
            return new BaseResponse<ICollection<ReadCategoryModel>> { Data = result.Select(e => e.ToModel()).ToList(), StatusCode = 200, Success = true };
        }
    }
}