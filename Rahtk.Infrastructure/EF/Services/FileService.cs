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
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            Directory.CreateDirectory(uploadsFolder);

            string path = $"/images/{uniqueFileName}";
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return path;

        }
    }
}

