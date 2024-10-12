using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
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
        public readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpPost]
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

        [HttpPut("{serviceId:guid}")]
        [Authorize(Roles = StaticUserRoles.AdminDoctor)]
        public async Task<ActionResult<ResponseDTO>> UpdateService([FromRoute] Guid serviceId, [FromBody] UpdateServiceDTO updateServiceDTO)
        {
            updateServiceDTO.ServiceId = serviceId;
            var responseDto = await _serviceService.UpdateService(User, updateServiceDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{serviceId:guid}/soft-delete")]
        [Authorize(Roles = StaticUserRoles.AdminDoctor)]
        public async Task<ActionResult<ResponseDTO>> DeleteService([FromRoute] Guid serviceId)
        {
            var responseDto = await _serviceService.DeleteService(User, serviceId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
