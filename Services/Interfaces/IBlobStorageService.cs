using Domain.Models;

namespace Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(FileUploadModel fileModel);
    }
}
