using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
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
    }
}
