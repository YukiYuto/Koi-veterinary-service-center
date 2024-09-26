using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllAppointments
        (
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var responseDto =
                await _appointmentService.GetAppointments(User, filterOn, filterQuery, sortBy, isAscending, pageNumber,
                    pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        
        
        [HttpGet]
        [Route("{appointmentId:guid}")]
        //[Authorize(Roles = StaticUserRoles.Customer)]
        public async Task<ActionResult<ResponseDTO>> GetAppointment
        (
            [FromRoute] Guid appointmentId
        )
        {
            var responseDto = await _appointmentService.GetAppointment(User, appointmentId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        
        
        [HttpPost]
        //[Authorize(Roles = StaticUserRoles.Customer)]
        public async Task<ActionResult<ResponseDTO>> CreateAppointment
        (
            [FromBody] CreateAppointmentDTO createAppointmentDto
        )
        {
            var responseDto = await _appointmentService.CreateAppointment(User, createAppointmentDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        //[Authorize(Roles = StaticUserRoles.Customer)]
        public async Task<ActionResult<ResponseDTO>> UpdateAppointment
        (
            [FromBody] UpdateAppointmentDTO updateAppointmentDto
        )
        {
            var responseDto = await _appointmentService.UpdateAppointment(User, updateAppointmentDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete]
        [Route("{appointmentId:guid}")]
        //[Authorize(Roles = StaticUserRoles.Customer)]
        public async Task<ActionResult<ResponseDTO>> DeleteAppointment
        (
            [FromRoute] Guid appointmentId
        )
        {
            var responseDto = await _appointmentService.DeleteAppointment(User, appointmentId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}