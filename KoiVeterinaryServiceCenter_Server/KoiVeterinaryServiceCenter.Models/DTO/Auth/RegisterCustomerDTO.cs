using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.DTO.Auth;

public class RegisterCustomerDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?""{}|<>])(?=.*\d).{6,}$",
        ErrorMessage = "Password must be at least 6 characters long, at least 1 number and contain at least one uppercase letter, one special character, and one number.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "ConfirmPassword is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [NotMapped]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "FullName is required")]
    public string FullName { get; set; } = null!;


    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    public string Gender { get; set; } = null!;

    [Required(ErrorMessage = "Birth date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Provided phone number is not valid.")]
    [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid.")]
    public string PhoneNumber { get; set; } = null!;
}