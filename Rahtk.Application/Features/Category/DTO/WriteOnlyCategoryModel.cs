using Microsoft.AspNetCore.Http;

namespace Rahtk.Application.Features.Category.DTO
{
	public record WriteOnlyCategoryModel(
        string ArabicName,
        string EnglishName,
        IFormFile file
    );
}

