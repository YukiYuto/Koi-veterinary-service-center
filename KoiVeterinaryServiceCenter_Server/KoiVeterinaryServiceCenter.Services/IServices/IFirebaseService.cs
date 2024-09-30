using KoiVeterinaryServiceCenter.Model.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IFirebaseService
    {
        Task<ResponseDTO> UploadImage(IFormFile file, string folder);
        Task<MemoryStream> GetImage(string filePath);
    }
}
