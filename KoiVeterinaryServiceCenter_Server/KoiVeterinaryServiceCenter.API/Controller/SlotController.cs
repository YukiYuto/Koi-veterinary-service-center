using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Model.DTO.Slot;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        /// <summary>
        /// Get all slots with optional filtering, sorting, and pagination.
        /// </summary>
        /// <param name="filterOn">Field to filter on.</param>
        /// <param name="filterQuery">Query for filtering.</param>
        /// <param name="sortBy">Field to sort by.</param>
        /// <param name="isAscending">Sort order.</param>
        /// <param name="pageNumber">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>A list of slots.</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllSlots(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var responseDto = await _slotService.GetSlots(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Get a specific slot by its ID.
        /// </summary>
        /// <param name="slotId">The ID of the slot.</param>
        /// <returns>The slot details.</returns>
        [HttpGet("{slotId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetSlot([FromRoute] Guid slotId)
        {
            var responseDto = await _slotService.GetSlot(User, slotId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Create a new slot.
        /// </summary>
        /// <param name="createSlotDto">The slot details.</param>
        /// <returns>The created slot details.</returns>
        [HttpPost]
        //[Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> CreateSlot([FromBody] CreateSlotDTO createSlotDto)
        {
            var responseDto = await _slotService.CreateSlot(User, createSlotDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Update an existing slot.
        /// </summary>
        /// <param name="updateSlotDto">The updated slot details.</param>
        /// <returns>The updated slot details.</returns>
        [HttpPut]
        [Authorize(Roles = StaticUserRoles.Doctor)]
        public async Task<ActionResult<ResponseDTO>> UpdateSlot(
            [FromBody] UpdateSlotDTO updateSlotDto)
        {
            var responseDto = await _slotService.UpdateSlot(User, updateSlotDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        /// <summary>
        /// Delete a slot by its ID.
        /// </summary>
        /// <param name="slotId">The ID of the slot to delete.</param>
        /// <returns>Confirmation of deletion.</returns>
        [HttpDelete("{slotId:guid}")]
        [Authorize(Roles = StaticUserRoles.Doctor)]
        public async Task<ActionResult<ResponseDTO>> DeleteSlot([FromRoute] Guid slotId)
        {
            var responseDto = await _slotService.DeleteSlot(User, slotId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}