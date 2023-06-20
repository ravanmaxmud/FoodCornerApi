using FoodCornerApi.Contracts.File;
using FoodCornerApi.Services.Abstracts;

namespace FoodCornerApi.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInFileSystem = GenerateUniqueFileName(formFile.FileName);

            var filePath = Path.Combine(directoryPath, imageNameInFileSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Some Things Went Wrong");
                throw;
            }

            return imageNameInFileSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName!);

            await Task.Run(() => File.Delete(deletePath));
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "Client/custom-files";

            switch (uploadDirectory)
            {
                case UploadDirectory.Slider:
                    return $"{initialSegment}/sliders/{fileName}";
                case UploadDirectory.Product:
                    return $"{initialSegment}/products/{fileName}";
                case UploadDirectory.Category:
                    return $"{initialSegment}/category/{fileName}";
                case UploadDirectory.TeamMembers:
                    return $"{initialSegment}/teamMembers/{fileName}";
                case UploadDirectory.Vidios:
                    return $"{initialSegment}/vidios/{fileName}";
                case UploadDirectory.Blogs:
                    return $"{initialSegment}/blogs/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "Client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Slider:
                    return Path.Combine(startPath, "sliders");
                case UploadDirectory.Product:
                    return Path.Combine(startPath, "products");
                case UploadDirectory.Category:
                    return Path.Combine(startPath, "category");
                case UploadDirectory.TeamMembers:
                    return Path.Combine(startPath, "teamMembers");
                case UploadDirectory.Vidios:
                    return Path.Combine(startPath, "vidios");
                case UploadDirectory.Blogs:
                    return Path.Combine(startPath, "blogs");
                default:
                    throw new Exception("Something went wrong");
            }
        }

    }
}
