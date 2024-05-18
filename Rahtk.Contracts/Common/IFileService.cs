using Microsoft.AspNetCore.Http;

namespace Rahtk.Contracts.Common
{
	public interface IFileService
	{
        Task<string> SaveFileAsync(IFormFile file);
    }
}

