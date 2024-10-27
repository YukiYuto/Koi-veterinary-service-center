using System.Globalization;
using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorRating;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorRatingController : ControllerBase
    {
        private readonly IDoctorRatingService _doctorRatingService;

        public DoctorRatingController(IDoctorRatingService doctorRatingService)
        {
            _doctorRatingService = doctorRatingService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRating(
                  [FromQuery] string? filterOn,
                  [FromQuery] string? filterQuery,
                  [FromQuery] string? sortBy,
                  [FromQuery] bool? isAscending,
                  [FromQuery] int pageNumber = 1,
                  [FromQuery] int pageSize = 10)
        {
            var user = HttpContext.User; // Get the currently authenticated user
            var response = await _doctorRatingService.GetAllRating(user, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(response); // Return the response from the service
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateRating([FromBody] CreateDoctorRatingDTO createDoctorRatingDTO)
        {
            var responseDto = await _doctorRatingService.CreateRating(User, createDoctorRatingDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetRatingByDoctorId(Guid doctorId)
        {
            var response = await _doctorRatingService.GetRatingByDoctorId(doctorId);
            return Ok(response); // Return the response from the service
        }
        [HttpGet("average/{doctorId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetRatesByDoctorId([FromRoute] Guid doctorId)
        {
            var responseDto = await _doctorRatingService.GetRatesByDoctorId(doctorId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("average")]
        public async Task<ActionResult<ResponseDTO>> GetAllRates()
        {
            var responseDto = await _doctorRatingService.GetAllRates();
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateRating([FromBody] UpdateDoctorRatingDTO updateDoctorRatingDTO)
        {
            var responseDto = await _doctorRatingService.UpdateRating(User, updateDoctorRatingDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{doctorRatingId:guid}")]
        public async Task<ActionResult<ResponseDTO>> DeleteRating([FromRoute] Guid doctorRatingId)
        {
            var responseDto = await _doctorRatingService.DeleteRating(User, doctorRatingId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
