using Microsoft.AspNetCore.Http;

namespace Rahtk.Application.Features.category
{
	public class WriteOnlyCategoryModel
	{
        public string ArabicName { get; set; }

        public string EnglishName { get; set; }

        public IFormFile file  { get; set; }
    }
}

