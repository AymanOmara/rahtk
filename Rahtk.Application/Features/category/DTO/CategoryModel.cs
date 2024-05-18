using Microsoft.AspNetCore.Http;

namespace Rahtk.Application.Features.category
{
	public class WriteOnlyCategoryModel
	{
        public string nameAr { get; set; }

        public string nameEn { get; set; }

        public IFormFile file  { get; set; }
    }
}

