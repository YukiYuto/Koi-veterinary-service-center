using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.API.Controllers
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

       
        [HttpPost]
       
        public async Task<ActionResult<ResponseDTO>> AddPetDisease([FromBody] AddPetDiseaseDTO addPetDiseaseDto)
        {
            var responseDto = await _petDiseaseService.AddPetDisease(User, addPetDiseaseDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

       
        [HttpGet("pet/{petId:guid}")]
  
        public async Task<ActionResult<ResponseDTO>> GetPetDiseasesByPetId(Guid petId)
        {
            var responseDto = await _petDiseaseService.GetPetDiseasesByPetId(User, petId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        
        public async Task<ActionResult<ResponseDTO>> UpdatePetDisease([FromBody] UpdatePetDiseaseDTO updatePetDiseaseDto)
        {
            var responseDto = await _petDiseaseService.UpdatePetDisease(User, updatePetDiseaseDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

       
        [HttpDelete("{petDiseaseId:guid}")]
       
        public async Task<ActionResult<ResponseDTO>> DeletePetDisease(Guid petDiseaseId)
        {
            var responseDto = await _petDiseaseService.DeletePetDisease(User, petDiseaseId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
