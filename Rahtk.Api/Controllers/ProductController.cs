using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.product;
using Rahtk.Application.Features.product.DTO;

namespace Rahtk.Api.Controllers
{
	[Route("api/[controller]")]
	//[Authorize]
	public class ProductController: ControllerBase
    {
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpPost("create-product")]
		public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel model) {

            var result = await _productService.CreateProduct(model);
			return result.toResult();
		}

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
			var result = await _productService.GetAllProducts();
			return result.toResult();
		}
    }
}

