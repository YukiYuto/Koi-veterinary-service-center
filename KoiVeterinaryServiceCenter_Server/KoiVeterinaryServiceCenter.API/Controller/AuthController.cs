using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Auth;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    /// <summary>
    /// Register a new doctor.
    /// </summary>
    /// <param name="registerDoctorDTO"></param>
    /// <returns></returns>
    [HttpPost("doctors")]
    public async Task<ActionResult<ResponseDTO>> SignUpDoctor([FromBody] RegisterDoctorDTO registerDoctorDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDTO
            {
                IsSuccess = false,
                Message = "Invalid input data.",
                Result = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        try
        {
            var result = await _authService.SignUpDoctor(registerDoctorDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
            {
                IsSuccess = false,
                Message = e.Message
            });
        }
    }

    /// <summary>
    /// Register a new customer.
    /// </summary>
    /// <param name="registerCustomerDTO"></param>
    /// <returns></returns>
    [HttpPost("customers")]
    public async Task<ActionResult<ResponseDTO>> SignUpCustomer([FromBody] RegisterCustomerDTO registerCustomerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDTO
            {
                IsSuccess = false,
                Message = "Invalid input data.",
                Result = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        try
        {
            var result = await _authService.SignUpCustomer(registerCustomerDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO
            {
                IsSuccess = false,
                Message = e.Message
            });
        }
    }

    /// <summary>
    /// Sign in with email and password.
    /// </summary>
    /// <param name="signDto"></param>
    /// <returns></returns>
    [HttpPost("sign-in")]
    public async Task<ActionResult<ResponseDTO>> SignIn([FromBody] SignDTO signDto)
    {
        var responseDto = await _authService.SignIn(signDto);
        return StatusCode(responseDto.StatusCode, responseDto);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("FetchUserByToken")]
    public async Task<IActionResult> FetchUserByToken(string token)
    {
        var result = await _authService.FetchUserByToken(token);
        var responseDto = await _authService.FetchUserByToken(token);
        return StatusCode(responseDto.StatusCode, responseDto);
    }

    /// <summary>
    /// Sign in with Google.
    /// </summary>
    /// <param name="signInByGoogleDto"></param>
    /// <returns></returns>
    [HttpPost("google/sign-in")]
    public async Task<ActionResult<ResponseDTO>> SignInByGoogle([FromBody] SignInByGoogleDTO signInByGoogleDto)
    {
        var response = await _authService.SignInByGoogle(signInByGoogleDto);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// Check if an email exists.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("check-email-exist")]
    public async Task<ActionResult<ResponseDTO>> CheckEmailExist([FromQuery] string email)
    {
        var responseDto = await _authService.CheckEmailExist(email);
        return StatusCode(responseDto.StatusCode, responseDto);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="avatarUploadDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("user/avatar")]
    [Authorize]
    public async Task<ActionResult<ResponseDTO>> UploadUserAvatar(AvatarUploadDTO avatarUploadDto)
    {
        var response = await _authService.UploadUserAvatar(avatarUploadDto.File, User);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("user/avatar")]
    //[Authorize]
    public async Task<IActionResult> GetUserAvatar()
    {
        var stream = await _authService.GetUserAvatar(User);
        if (stream is null)
        {
            return NotFound("User avatar does not exist!");
        }

        return File(stream, "image/png");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("send-verify-email")]
    public async Task<ActionResult<ResponseDTO>> SendVerifyEmail([FromBody] SendVerifyEmailDTO email)
    {
        var user = await _userManager.FindByEmailAsync(email.Email);
        if (user.EmailConfirmed)
        {
            return new ResponseDTO()
            {
                IsSuccess = true,
                Message = "Your email has been confirmed",
                StatusCode = 200,
                Result = email
            };
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink =
            $"{Request.Scheme}://{Request.Host}/user/sign-in/verify-email?userId={Uri.EscapeDataString(user.Id)}&token={Uri.EscapeDataString(token)}";

        var responseDto = await _authService.SendVerifyEmail(user.Email, confirmationLink);

        return StatusCode(responseDto.StatusCode, responseDto);
    }

    [HttpPost]
    [Route("verify-email")]
    [ActionName("verify-email")]
    public async Task<ActionResult<ResponseDTO>> VerifyEmail(
        [FromQuery] string userId,
        [FromQuery] string token)
    {
        var responseDto = await _authService.VerifyEmail(userId, token);
        return StatusCode(responseDto.StatusCode, responseDto);
    }
}