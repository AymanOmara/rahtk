using Rahtk.Application.Features.category.DTO;
using Rahtk.Application.Features.category.mappers;
using Rahtk.Contracts.Common;
using Rahtk.Domain.Features.Product;
using Rahtk.Shared.Models;

namespace Rahtk.Application.Features.category
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitofWork;
        
        public CategoryService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category)
        {
            var categoryEntity = category.ToEntity();

            var result =  await _unitofWork.Category.CreateCategory(category.file, categoryEntity);

            return new BaseResponse<CategoryEntity> { data = result, statusCode = 200,success = true};
        }

        public async Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories(string email)
        {
            var result = await _unitofWork.Category.GetAllCategories(email);
            return new BaseResponse<ICollection<ReadCategoryModel>> {data = result.Select(e=>e.ToModel()).ToList(),statusCode = 200,success = true };
        }
    }
}