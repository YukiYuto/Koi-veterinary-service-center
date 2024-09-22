using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// This API for feature Sign Up For Doctor.
        /// </summary>
        /// <param name="registerDoctorDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignUpDoctor")]
        public async Task<ActionResult<ResponseDTO>> SignUpDoctor([FromBody] RegisterDoctorDTO registerDoctorDTO)
        {
            var responseDto = new ResponseDTO();
            if (!ModelState.IsValid)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Invalid input data.";
                responseDto.Result = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(responseDto);
            }
            try
            {
                var result = await _authService.SignUpDoctor(registerDoctorDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseDto);
            }

        }
        
        /// <summary>
        /// This API for feature Sign Up For Customer.
        /// </summary>
        /// <param name="registerCustomerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignUpCustomer")]
        public async Task<ActionResult<ResponseDTO>> SignUpCustomer([FromBody] RegisterCustomerDTO registerCustomerDTO)
        {
            var responseDto = new ResponseDTO();
            if (!ModelState.IsValid)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Invalid input data.";
                responseDto.Result = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(responseDto);
            }
            try
            {
                var result = await _authService.SignUpCustomer(registerCustomerDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseDto);
            }
        }
        
        
        /// <summary>
        /// his API for case sign in.
        /// </summary>
        /// <param name="signDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sign-in")]
        public async Task<ActionResult<ResponseDTO>> SignIn([FromBody] SignDTO signDto)
        {
            var responseDto = await _authService.SignIn(signDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
