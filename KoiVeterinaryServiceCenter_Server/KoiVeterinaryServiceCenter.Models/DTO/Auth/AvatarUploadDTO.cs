using System.ComponentModel.DataAnnotations;
using KoiVeterinaryServiceCenter.Utility.ValidationAttribute;
using Microsoft.AspNetCore.Http;

namespace KoiVeterinaryServiceCenter.Models.DTO.Auth;

public class AvatarUploadDTO
{
    [Required]
    [MaxFileSize(1)]
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
    public IFormFile File { get; set; }
}