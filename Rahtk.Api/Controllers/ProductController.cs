using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rahtk.Api.Utils;
using Rahtk.Application.Features.Product.DTO;
using Rahtk.Application.Features.Product.Service;

namespace Rahtk.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel model)
        {
            var result = await productService.CreateProduct(model);
            return result.ToResult();
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productService.GetAllProducts();
            return result.ToResult();
        }

        [HttpPost("add-to-favorite")]
        public async Task<IActionResult> AddToFavorite(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await productService.AddToFavorite(userId, productId);
            return result.ToResult();
        }

        [HttpPost("remove-favorite")]
        public async Task<IActionResult> RemoveFavorite(int productId)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await productService.RemoveFavorite(email, productId);
            return result.ToResult();
        }

        [HttpGet("get-favorite")]
        public async Task<IActionResult> GetFavorites()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await productService.GetFavorites(email);
            return result.ToResult();
        }

        [HttpGet("product-details")]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await productService.ProductDetails(email, productId);
            return result.ToResult();
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await productService.DeleteProduct(productId);
            return result.ToResult();
        }
    }
}