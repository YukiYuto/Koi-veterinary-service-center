using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorServicesController : ControllerBase
    {
        public readonly IDoctorServicesService _doctorServicesService;
        public DoctorServicesController(IDoctorServicesService doctorServicesService)
        {
            _doctorServicesService = doctorServicesService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateDoctorService([FromBody] CreateDoctorServicesDTO createDoctorServicesDTO)
        {
            var responseDto = await _doctorServicesService.CreateDoctorService(User, createDoctorServicesDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{serviceId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetDoctorSeriveById([FromRoute] Guid serviceId)
        {
            var responseDto = await _doctorServicesService.GetDoctorServiceById(User, serviceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{doctorServiceId:guid}")]
        public async Task<ActionResult<ResponseDTO>> UpdateDoctorService([FromRoute] Guid doctorServiceId, [FromBody] UpdateDoctorServicesDTO updateDoctorServicesDTO)
        {
            updateDoctorServicesDTO.DoctorServiceId = doctorServiceId;
            var responseDto = await _doctorServicesService.UpdateDoctorService(User, updateDoctorServicesDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{doctorServiceId:guid}")]
        public async Task<ActionResult<ResponseDTO>> DeleteDoctorService([FromRoute] Guid doctorServiceId)
        {
            var responseDto = await _doctorServicesService.DeleteDoctorService(User, doctorServiceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
