using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.product;
using Rahtk.Application.Features.product.DTO;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel model)
        {
            var result = await _productService.CreateProduct(model);
            return result.ToResult();
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return result.ToResult();
        }
        [HttpPost("add-to-favorite")]
        public async Task<IActionResult> AddToFavorite(int ProductId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _productService.AddToFavorite(userId, ProductId);
            return result.ToResult();
        }
        [HttpPost("remove-favorite")]
        public async Task<IActionResult> RemoveFavorite(int ProductId)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _productService.RemoveFavorite(email, ProductId);
            return result.ToResult();
        }
        [HttpGet("get-favorite")]
        public async Task<IActionResult> GetFavorites()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _productService.GetFavorites(email);
            return result.ToResult();
        }
        [HttpGet("product-details")]
        public async Task<IActionResult> GetProductDetails(int ProductId)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _productService.ProductDetails(email, ProductId);
            return result.ToResult();
        }
    }
}