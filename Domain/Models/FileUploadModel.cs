using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class FileUploadModel
    {
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? FileCategory { get; set; }
    }
}
