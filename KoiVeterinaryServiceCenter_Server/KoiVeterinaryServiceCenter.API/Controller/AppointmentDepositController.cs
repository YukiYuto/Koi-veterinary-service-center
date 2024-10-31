using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.AppointmentDeposit;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDepositController : ControllerBase
    {
        private readonly IAppointmentDepositService _appointmentDepositService;
        
        public AppointmentDepositController(IAppointmentDepositService appointmentDepositService)
        {
            _appointmentDepositService = appointmentDepositService;
        }
        
        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateAppointmentDeposit([FromBody] CreateAppointmentDepositDTO createAppointmentDepositDto)
        {
            var responseDto = await _appointmentDepositService.CreateAppointmentDeposit(User, createAppointmentDepositDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        
        [HttpGet("{appointmentId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetAppointmentDepositByAppointmentId(Guid appointmentId)
        {
            var responseDto = await _appointmentDepositService.GetAppointmentDepositByAppointmentId(User, appointmentId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        
        [HttpDelete("{appointmentDepositId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> DeleteAppointmentDeposit(Guid appointmentDepositId)
        {
            var responseDto = await _appointmentDepositService.DeleteAppointmentDeposit(User, appointmentDepositId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
