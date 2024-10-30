namespace KoiVeterinaryServiceCenter.Models.DTO.Auth;

public class SignResponseDTO
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}