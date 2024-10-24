using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Auth;
using Microsoft.AspNetCore.Http;

namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface IAuthService
{
    Task<ResponseDTO> SignUpDoctor(RegisterDoctorDTO registerDoctorDTO);
    Task<ResponseDTO> SignUpCustomer(RegisterCustomerDTO registerCustomerDTO);
    Task<ResponseDTO> SignIn(SignDTO signDto);
    //Task<ResponseDTO> ForgotPassword(ForgotPasswordDTO forgotPasswordDto);
    Task<ResponseDTO> SignInByGoogle(SignInByGoogleDTO signInByGoogleDto);
    Task<ResponseDTO> CheckEmailExist(string email);
    Task<ResponseDTO> UploadUserAvatar(IFormFile file, ClaimsPrincipal user);
    Task<MemoryStream> GetUserAvatar(ClaimsPrincipal user);
    Task<ResponseDTO> SendVerifyEmail(string email, string confirmationLink);
    Task<ResponseDTO> VerifyEmail(string userId, string token);
}