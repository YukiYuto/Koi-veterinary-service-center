using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.PetService;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetServiceController : ControllerBase
    {
        private readonly IPetServiceService _petServiceService;
        public PetServiceController(IPetServiceService petServiceService)
        {
            _petServiceService = petServiceService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreatePetService([FromBody] CreatePetServiceDTO createPetServiceDTO)
        {
            var responseDto = await _petServiceService.CreatePetService(User, createPetServiceDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdatePetService([FromBody] UpdatePetServiceDTO updatePetServiceDTO)
        {
            var responseDto = await _petServiceService.UpdatePetService(User, updatePetServiceDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
