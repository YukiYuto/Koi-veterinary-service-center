using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Models.DTO.Auth;

public class SendVerifyEmailDTO
{
    [EmailAddress] public string Email { get; set; } = null!;
}