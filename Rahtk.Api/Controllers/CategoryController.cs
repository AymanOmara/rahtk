using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.Category;
using Rahtk.Application.Features.Category.DTO;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryApplication) : ControllerBase
    {
        [HttpPost("add-category")]
        public async Task<IActionResult> CraeteCategory([FromForm] WriteOnlyCategoryModel category)
        {
            var result = await categoryApplication.CreateCategory(category);
            return result.ToResult();
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await categoryApplication.GetAllCategories(email);
            return result.ToResult();
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var result = await categoryApplication.DeleteCategory(categoryId);
            return result.ToResult();
        }
    }
}