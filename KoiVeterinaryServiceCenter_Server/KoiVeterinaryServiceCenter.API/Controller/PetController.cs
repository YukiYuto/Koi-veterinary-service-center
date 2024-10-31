using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pet;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetDTO createPetDTO)
        {
            var user = HttpContext.User;
            var response = await _petService.CreatePet(user, createPetDTO);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var user = HttpContext.User;
            var response = await _petService.GetAllPets(user, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{petId:guid}")]
        public async Task<IActionResult> GetPetById(Guid petId)
        {
            var response = await _petService.GetPetById(petId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetPetsByCustomerId(string customerId)
        {
            var user = HttpContext.User;
            var response = await _petService.GetPetsByCustomerId(user, customerId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePet([FromBody] UpdatePetDTO updatePetDTO)
        {
            var user = HttpContext.User;
            var response = await _petService.UpdatePet(user, updatePetDTO);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{petId:guid}")]
        public async Task<IActionResult> DeletePet(Guid petId)
        {
            var user = HttpContext.User;
            var response = await _petService.DeletePet(user, petId);

            return StatusCode(response.StatusCode, response);
        }

       
        [HttpPost("avatar")]
        
        public async Task<ActionResult<ResponseDTO>> UploadPostAvatar(IFormFile file)
        {
            var responseDto = await _petService.UploadPetAvatar(file, User);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
