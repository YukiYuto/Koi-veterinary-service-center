using System.Security.Claims;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Model.DTO.Service;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public readonly IServicesService _serviceService;
        public ServiceController(IServicesService serviceService)
        {
            _serviceService = serviceService;
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
            var responseDto = await _serviceService.GetAll(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> CreateService([FromBody] CreateServiceDTO createServiceDTO)
        {
            var responseDto = await _serviceService.CreateService(User, createServiceDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{serviceId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetService([FromRoute] Guid serviceId)
        {
            var responseDto = await _serviceService.GetServiceById(User, serviceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> UpdateService(UpdateServiceDTO updateServiceDTO)
        {
            var responseDto = await _serviceService.UpdateService(User, updateServiceDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{serviceId:guid}/soft-delete")]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> DeleteService([FromRoute] Guid serviceId)
        {
            var responseDto = await _serviceService.DeleteService(User, serviceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}