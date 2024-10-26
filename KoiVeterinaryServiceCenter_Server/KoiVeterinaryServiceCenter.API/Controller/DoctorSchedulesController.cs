using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSchedulesController : ControllerBase
    {
        private readonly IDoctorSchedulesService _doctorSchedulesService;
        public DoctorSchedulesController(IDoctorSchedulesService doctorSchedulesService)
        {
            _doctorSchedulesService = doctorSchedulesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllDoctorSchedules(
            [FromQuery] string? filterOn,

            [FromQuery] string? filterQuery,

            [FromQuery] string? sortBy,

            [FromQuery] bool? isAscending,

            [FromQuery] int pageNumber,

            [FromQuery] int pageSize)
        {
            var responseDto = await _doctorSchedulesService.GetAll(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> CreateDoctorSchedule([FromBody] CreateDoctorSchedulesDTO createDoctorSchedulesDTO)
        {
            var responseDto = await _doctorSchedulesService.CreateDoctorSchedule(User, createDoctorSchedulesDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        [HttpGet]
        [Route("{doctorScheduleId:Guid}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetDoctorScheduleById([FromRoute] Guid doctorScheduleId)
        {
            var responseDto = await _doctorSchedulesService.GetDoctorScheduleById(User, doctorScheduleId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        [HttpPut]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> UpdateDoctorSchedule([FromBody] UpdateDoctorSchedulesDTO updateDoctorSchedulesDTO)
        {
            var responseDto = await _doctorSchedulesService.UpdateDoctorScheduleById(User, updateDoctorSchedulesDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{doctorScheduleId:Guid}/soft-delete")]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> DeleteDoctorSchedule([FromRoute] Guid doctorScheduleId)
        {
            var responseDto = await _doctorSchedulesService.DeleteDoctorScheduleById(User, doctorScheduleId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet]
        [Route("doctor/{doctorId:Guid}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetDoctorScheduleByDoctorId([FromRoute] Guid doctorId)
        {
            var responseDto = await _doctorSchedulesService.GetDoctorScheduleByDoctorId(User, doctorId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
