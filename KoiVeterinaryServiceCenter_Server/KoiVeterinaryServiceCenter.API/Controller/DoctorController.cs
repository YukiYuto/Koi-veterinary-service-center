﻿using System.Runtime.ConstrainedExecution;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Doctor;
using KoiVeterinaryServiceCenter.Models.DTO.Slot;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllDocTor(
            [FromQuery] string? filterOn,

            [FromQuery] string? filterQuery,

            [FromQuery] string? sortBy,

            [FromQuery] bool? isAscending,

            [FromQuery] int pageNumber,

            [FromQuery] int pageSize)
        {
            var responseDto = await _doctorService.GetAll(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("get-by-slot")]
        public async Task<ActionResult<ResponseDTO>> GetAllDoctorBySlot([FromQuery] GetAllDoctorBySlotDTO getAllDoctorBySlotDTO)
        {
            var responseDto = await _doctorService.GetAllDoctorBySlot(User, getAllDoctorBySlotDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Get doctor by ID
        /// </summary>
        /// <param name="doctorId">The ID of the doctor</param>
        /// <returns>A response with doctor details</returns>
        [HttpGet]
        [Route("{doctorId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetDoctorById([FromRoute] Guid doctorId)
        {
            var responseDto = await _doctorService.GetDoctorById(User, doctorId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        [Authorize(Roles = StaticUserRoles.AdminDoctor)]
        public async Task<ActionResult<ResponseDTO>> UpdateDoctor([FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            var responseDto = await _doctorService.UpdateDoctorById(User, updateDoctorDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{doctorId:guid}/soft-delete")]
        public async Task<ActionResult<ResponseDTO>> DeleteDoctorById([FromRoute] Guid doctorId)
        {
            var responseDto = await _doctorService.DeleteDoctorById(User, doctorId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost("google-meet-links")]
        [Authorize(Roles = StaticUserRoles.AdminStaff)]
        public async Task<ActionResult<ResponseDTO>> CreateGoogleMeetLink([FromBody] GoogleMeetLinkDTO googleMeetLinkDTO)
        {
            var responseDto = await _doctorService.CreateGoogleMeetLink(User, googleMeetLinkDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("google-meet-links")]
        [Authorize(Roles = StaticUserRoles.AdminStaff)]
        public async Task<ActionResult<ResponseDTO>> UpdateGoogleMeetLink([FromBody] GoogleMeetLinkDTO googleMeetLinkDTO)
        {
            var responseDto = await _doctorService.UpdateGoogleMeetLink(User, googleMeetLinkDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
