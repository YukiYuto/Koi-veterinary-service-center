using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;

using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllDiseases
            (
                [FromQuery] string? filterOn,
                [FromQuery] string? filterQuery,
                [FromQuery] string? sortBy,
                [FromQuery] bool? isAscending,
                [FromQuery] int pageNumber,
                [FromQuery] int pageSize
            )
        {
            var responseDto = await _diseaseService.GetAllDiseases(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{diseaseId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetDiseaseById(Guid diseaseId)
        {
            var responseDto = await _diseaseService.GetDiseaseById(diseaseId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateDisease(CreateDiseaseDTO createDiseaseDTO)
        {
            var responseDto = await _diseaseService.CreateDisease(User, createDiseaseDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateDisease(UpdateDiseaseDTO updateDiseaseDTO)
        {
            var responseDto = await _diseaseService.UpdateDisease(User, updateDiseaseDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{diseaseId:guid}")]
        public async Task<ActionResult<ResponseDTO>> DeleteDisease(Guid diseaseId)
        {
            var responseDto = await _diseaseService.DeleteDisease(User, diseaseId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
