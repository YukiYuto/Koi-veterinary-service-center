using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Get all appointments with optional filtering, sorting, and pagination.
        /// </summary>
        /// <param name="filterOn">Field to filter on.</param>
        /// <param name="filterQuery">Query for filtering.</param>
        /// <param name="sortBy">Field to sort by.</param>
        /// <param name="isAscending">Sort order.</param>
        /// <param name="pageNumber">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>A list of appointments.</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllAppointments(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var responseDto = await _appointmentService.GetAppointments(User, filterOn, filterQuery, sortBy, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Get a specific appointment by its ID.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment.</param>
        /// <returns>The appointment details.</returns>
        [HttpGet("{appointmentId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetAppointment([FromRoute] Guid appointmentId)
        {
            var responseDto = await _appointmentService.GetAppointment(User, appointmentId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Create a new appointment.
        /// </summary>
        /// <param name="createAppointmentDto">The appointment details.</param>
        /// <returns>The created appointment details.</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDto)
        {
            var responseDto = await _appointmentService.CreateAppointment(User, createAppointmentDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Update an existing appointment.
        /// </summary>
        /// <param name="updateAppointmentDto">The updated appointment details.</param>
        /// <returns>The updated appointment details.</returns>
        [HttpPut("{appointmentId:guid}")]
        public async Task<ActionResult<ResponseDTO>> UpdateAppointment(
            [FromRoute] Guid appointmentId,
            [FromBody] UpdateAppointmentDTO updateAppointmentDto)
        {
            updateAppointmentDto.AppointmentId = appointmentId; // Assigning the route ID to the DTO
            var responseDto = await _appointmentService.UpdateAppointment(User, updateAppointmentDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Delete an appointment by its ID.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to delete.</param>
        /// <returns>Confirmation of deletion.</returns>
        [HttpDelete("{appointmentId:guid}")]
        public async Task<ActionResult<ResponseDTO>> DeleteAppointment([FromRoute] Guid appointmentId)
        {
            var responseDto = await _appointmentService.DeleteAppointment(User, appointmentId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
