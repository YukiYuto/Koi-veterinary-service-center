using KoiVeterinaryServiceCenter.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface IFirebaseService
{
    Task<ResponseDTO> UploadImage(IFormFile file, string folder);
    Task<ResponseDTO> UploadImagePost(IFormFile file, string folder);
    Task<ResponseDTO> UploadImagePet(IFormFile file, string folder);

    Task<ResponseDTO> UploadImagePool (IFormFile file, string folder);
    Task<MemoryStream> GetImage(string filePath);
}