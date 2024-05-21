using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CategoryEntity>> CreateCategory(WriteOnlyCategoryModel category)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(category);

            var result =  await _unitofWork.Category.CreateCategory(category.file, categoryEntity);

            return new BaseResponse<CategoryEntity> { data = result, statusCode = 200,success = true};
        }

        public async Task<BaseResponse<ICollection<ReadCategoryModel>>> GetAllCategories()
        {
            var result = await _unitofWork.Category.GetAllCategories();
            return new BaseResponse<ICollection<ReadCategoryModel>> {data = result.Select(e=>e.ToModel()).ToList(),statusCode = 200,success = true };
        }
    }
}

