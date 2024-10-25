using System.Security.Claims;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorServices;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAll
            (
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
            )
        {
            var responseDto = await _doctorServicesService.GetAll(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> CreateDoctorService([FromBody] CreateDoctorServicesDTO createDoctorServicesDTO)
        {
            var responseDto = await _doctorServicesService.CreateDoctorService(User, createDoctorServicesDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{serviceId:guid}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetDoctorSeriveById([FromRoute] Guid serviceId)
        {
            var responseDto = await _doctorServicesService.GetDoctorServiceById(User, serviceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{doctorServiceId:guid}")]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> UpdateDoctorService([FromRoute] Guid doctorServiceId, [FromBody] UpdateDoctorServicesDTO updateDoctorServicesDTO)
        {
            updateDoctorServicesDTO.DoctorServiceId = doctorServiceId;
            var responseDto = await _doctorServicesService.UpdateDoctorService(User, updateDoctorServicesDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{doctorServiceId:guid}")]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> DeleteDoctorService([FromRoute] Guid doctorServiceId)
        {
            var responseDto = await _doctorServicesService.DeleteDoctorService(User, doctorServiceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
