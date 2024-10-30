using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.PetService;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetDiseaseController : ControllerBase
    {
        private readonly IPetDiseaseService _petDiseaseService;

        public PetDiseaseController(IPetDiseaseService petDiseaseService)
        {
            _petDiseaseService = petDiseaseService;
        }

        [HttpGet("by-pet/{petId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetPetDiseasesByPetId([FromRoute] Guid petId)
        {
            var responseDto = await _petDiseaseService.GetPetDiseasesByPetId(petId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("by-disease/{diseaseId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetPetDiseasesByDiseaseId([FromRoute] Guid diseaseId)
        {
            var responseDto = await _petDiseaseService.GetPetDiseasesByDiseaseId(diseaseId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddPetDisease([FromBody] CreatePetDiseaseDTO createPetDiseaseDTO)
        {
            var responseDto = await _petDiseaseService.AddPetDisease(User, createPetDiseaseDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdatePetDisease([FromBody] UpdatePetDiseaseDTO updatePetDiseaseDTO)
        {
            var responseDto = await _petDiseaseService.UpdatePetDisease(User, updatePetDiseaseDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}

        