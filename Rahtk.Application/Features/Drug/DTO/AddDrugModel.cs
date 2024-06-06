using Microsoft.AspNetCore.Http;

namespace Rahtk.Application.Features.Drug.DTO
{
	public class AddDrugModel
	{
        public string Name { get; set; } = string.Empty;

        public IFormFile Image { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}