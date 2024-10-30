using Google.Cloud.Storage.V1;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class FirebaseService : IFirebaseService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "koiveterinaryservicecent-33eaa.appspot.com";

    public FirebaseService(StorageClient storageClient)
    {
        _storageClient = storageClient;
    }

    /// <summary>
    /// This method for get an image from firebase storage bucket
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public async Task<MemoryStream> GetImage(string filePath)
    {
        try
        {
            MemoryStream memoryStream = new MemoryStream();

            await _storageClient.DownloadObjectAsync(_bucketName, filePath, memoryStream);

            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
        catch (Exception e)
        {
            return null;
        }
    }


    /// <summary>
    /// This method for upload an image to firebase storage bucket
    /// </summary>
    /// <param name="file"></param>
    /// <param name="folder"></param>
    /// <returns></returns>
    public async Task<ResponseDTO> UploadImage(IFormFile file, string folder)
    {
        if (file is null || file.Length == 0)
        {
            return new ResponseDTO()
            {
                IsSuccess = false,
                StatusCode = 400,
                Message = "File is empty!"
            };
        }
                                                                    
        var fileName = $"{Guid.NewGuid()}_{file.FileName}"; // Tạo tên file duy nhất
        var filePath = $"{folder}/{fileName}"; // Đường dẫn đầy đủ cho tệp trong Firebase

        // Khởi tạo luồng đọc từ file
        await using (var stream = file.OpenReadStream())
        {
            // Upload file lên Firebase
            var result = await _storageClient.UploadObjectAsync(
                _bucketName,
                filePath,
                file.ContentType, // Loại MIME của tệp
                stream,
                new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead // Để tệp có thể truy cập công khai
                }
            );
        }

        // Tạo URL công khai cho hình ảnh vừa tải lên
        string publicUrl = $"https://storage.googleapis.com/{_bucketName}/{filePath}";

        return new ResponseDTO()
        {
            IsSuccess = true,
            StatusCode = 200,
            Result = publicUrl, // Trả về URL công khai
            Message = "Upload image successfully!"
        };
    }

    public async Task<ResponseDTO> UploadImagePet(IFormFile file, string folder)
    {
        if (file is null || file.Length == 0)
        {
            return new ResponseDTO()
            {
                IsSuccess = false,
                StatusCode = 400,
                Message = "File is empty!"
            };
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}"; // Create a unique file name
        var filePath = $"{folder}/{fileName}"; // Full path for the file in Firebase

        // Initialize the stream from the file
        await using (var stream = file.OpenReadStream())
        {
            // Upload the file to Firebase
            var result = await _storageClient.UploadObjectAsync(
                _bucketName,
                filePath,
                file.ContentType, // File MIME type
                stream,
                new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead // Make the file publicly accessible
                }
            );
        }

        // Create the public URL for the uploaded image
        string publicUrl = $"https://storage.googleapis.com/{_bucketName}/{filePath}";

        return new ResponseDTO()
        {
            IsSuccess = true,
            StatusCode = 200,
            Result = publicUrl, // Return the public URL
            Message = "Upload image successfully!"
        };
    }
    public async Task<ResponseDTO> UploadImagePost(IFormFile file, string folder)
    {
        if (file is null || file.Length == 0)
        {
            return new ResponseDTO()
            {
                IsSuccess = false,
                StatusCode = 400,
                Message = "File is empty!"
            };
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}"; // Tạo tên file duy nhất
        var filePath = $"{folder}/{fileName}"; // Đường dẫn đầy đủ cho tệp trong Firebase

        // Khởi tạo luồng đọc từ file
        await using (var stream = file.OpenReadStream())
        {
            // Upload file lên Firebase
            var result = await _storageClient.UploadObjectAsync(
                _bucketName,
                filePath,
                file.ContentType, // Loại MIME của tệp
                stream,
                new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead // Để tệp có thể truy cập công khai
                }
            );
        }

        // Tạo URL công khai cho hình ảnh vừa tải lên
        string publicUrl = $"https://storage.googleapis.com/{_bucketName}/{filePath}";

        return new ResponseDTO()
        {
            IsSuccess = true,
            StatusCode = 200,
            Result = publicUrl, // Trả về URL công khai
            Message = "Upload image successfully!"
        };
    }
}