using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Mvc;


namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Get doctor by ID
        /// </summary>
        /// <param name="doctorId">The ID of the doctor</param>
        /// <returns>A response with doctor details</returns>
        [HttpGet]
        [Route("{doctorId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetDoctorById([FromRoute] Guid doctorId)
        {
            var responseDto = await _doctorService.GetDoctorById(doctorId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}