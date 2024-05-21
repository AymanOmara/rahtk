using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.category;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryApplication;
        public CategoryController(ICategoryService categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> CraeteCategory([FromForm]WriteOnlyCategoryModel category)
        {
            var result = await _categoryApplication.CreateCategory(category);
            return result.toResult();
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryApplication.GetAllCategories();
            return result.toResult();
        }
    }
}