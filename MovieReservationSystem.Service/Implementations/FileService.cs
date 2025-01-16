using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Data.Resources;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly string _imagesFolder = "images";
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _maxFileSize = 5 * 1024 * 1024; //5MB

        public FileService(string basePath)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        }

        public async Task<string> SaveImageAsync(IFormFile file, string featureFolder)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (file.Length == 0)
                throw new ArgumentException(SharedResourcesKeys.FileEmpty, nameof(file));

            if (file.Length > _maxFileSize)
                throw new ArgumentException($"{SharedResourcesKeys.FileSizeLimit} {_maxFileSize / 1024 / 1024}MB");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                throw new ArgumentException($"{SharedResourcesKeys.FileTypeNotAllowed} {string.Join(", ", _allowedExtensions)}");

            var uploadsFolder = Path.Combine(_basePath, _imagesFolder, featureFolder);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                Directory.CreateDirectory(uploadsFolder);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Path.Combine(_imagesFolder, featureFolder, uniqueFileName);
            }
            catch (Exception ex)
            {
                throw new IOException($"{SharedResourcesKeys.SavingError} {file.FileName}: {ex.Message}", ex);
            }
        }

        public void DeleteImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentNullException(nameof(imagePath));

            var fullPath = GetFullPath(imagePath);

            if (!File.Exists(fullPath))
                return; // File doesn't exist, nothing to delete

            try
            {
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                throw new IOException($"{SharedResourcesKeys.DeletingError} {imagePath}: {ex.Message}", ex);
            }
        }

        public bool FileExists(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return false;

            return File.Exists(GetFullPath(imagePath));
        }

        public string GetFullPath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentNullException(nameof(imagePath));

            return Path.Combine(_basePath, imagePath);
        }
        public string GetDefaultImagePath()
        {
            return "/assets/default.jpg";
        }

        public async Task<string> ReplaceImageAsync(string oldImagePath, IFormFile newImage, string featureFolder)
        {
            try
            {
                string newImagePath = await SaveImageAsync(newImage, featureFolder);

                if (FileExists(oldImagePath))
                    DeleteImage(oldImagePath);

                return newImagePath;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
