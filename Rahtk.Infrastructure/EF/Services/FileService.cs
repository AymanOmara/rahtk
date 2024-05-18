using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Rahtk.Contracts.Common;

namespace Rahtk.Infrastructure.EF.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var path = Path.Combine(_environment.WebRootPath, "images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }
    }
}

