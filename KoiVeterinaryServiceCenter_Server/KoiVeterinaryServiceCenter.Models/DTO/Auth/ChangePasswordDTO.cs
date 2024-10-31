using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Models.DTO.Auth;

public class ChangePasswordDTO
{
    [Required]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; } = null!;
}