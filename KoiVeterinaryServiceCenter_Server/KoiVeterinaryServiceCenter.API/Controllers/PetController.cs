﻿using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
       
        public async Task<ActionResult<ResponseDTO>> CreatePet([FromBody] CreatePetDTO createPetDto)
        {

            var responseDto = await _petService.CreatePet(User, createPetDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{petId:guid}")]
        
        public async Task<ActionResult<ResponseDTO>> GetPet(Guid petId)
        {
            var responseDto = await _petService.GetPet(User, petId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{petId:guid}")]
       
        public async Task<ActionResult<ResponseDTO>> UpdatePet(Guid petId, [FromBody] UpdatePetDTO updatePetDto)
        {
            var responseDto = await _petService.UpdatePet(petId,  User, updatePetDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }


        [HttpDelete("{petId:guid}")]
       
        public async Task<ActionResult<ResponseDTO>> DeletePet(Guid petId)
        {
            var responseDto = await _petService.DeletePet(User, petId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}