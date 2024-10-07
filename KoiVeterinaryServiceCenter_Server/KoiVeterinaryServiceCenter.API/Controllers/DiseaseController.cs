using KoiVeterinaryServiceCenter.Model.DTO;
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
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateDisease([FromBody] CreateDiseaseDTO createDiseaseDto)
        {
            var responseDto = await _diseaseService.CreateDisease(createDiseaseDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{diseaseId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetDisease(Guid diseaseId)
        {
            var responseDto = await _diseaseService.GetDisease(diseaseId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{diseaseId:guid}")]
        public async Task<ActionResult<ResponseDTO>> UpdateDisease(Guid diseaseId, [FromBody] UpdateDiseaseDTO updateDiseaseDto)
        {
            var responseDto = await _diseaseService.UpdateDisease(diseaseId, updateDiseaseDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{diseaseId:guid}")]
        public async Task<ActionResult<ResponseDTO>> DeleteDisease(Guid diseaseId)
        {
            var responseDto = await _diseaseService.DeleteDisease(diseaseId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllDiseases()
        {
            var responseDto = await _diseaseService.GetAllDisease();
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}