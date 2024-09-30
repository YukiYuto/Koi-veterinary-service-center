using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Model.DTO;

public class SendVerifyEmailDTO
{
    [EmailAddress]
    public string Email { get; set; }
}