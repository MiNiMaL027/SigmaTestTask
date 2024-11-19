using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Filters;
using Services.Interfaces;

namespace SigmaTestTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NotImplExceptionFilter]
    public class FileController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public FileController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadModel fileModel)
        {                     
            return Ok(await _blobStorageService.UploadFileAsync(fileModel));        
        }
    }
}
