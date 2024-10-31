namespace KoiVeterinaryServiceCenter.Models.DTO.Auth;

public class ChangePasswordDTO
{
    public string oldPassword { get; set; }
    public string newPassword { get; set; }
    public string confirmPassword { get; set; }
}