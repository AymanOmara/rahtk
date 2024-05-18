using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.category;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;
        public ProductsController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpPost]
        public async Task<IActionResult> CraeteCategory([FromForm]WriteOnlyCategoryModel category)
        {
            var result = await _categoryApplication.CreateCategory(category);
            return result.toResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryApplication.GetAllCategories();
            return result.toResult();
        }
    }
}

