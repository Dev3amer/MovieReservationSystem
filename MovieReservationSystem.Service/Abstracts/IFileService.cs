using Microsoft.AspNetCore.Http;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile file, string featureFolder);
        void DeleteImage(string imagePath);
        bool FileExists(string imagePath);
        string GetFullPath(string imagePath);
        Task<string> ReplaceImageAsync(string oldImagePath, IFormFile newImage, string featureFolder);
        public string GetDefaultImagePath();
    }
}
