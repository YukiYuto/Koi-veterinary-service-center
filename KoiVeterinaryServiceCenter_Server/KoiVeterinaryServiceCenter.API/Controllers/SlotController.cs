using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : Controller
    {
        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        [Route("{slotId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetSlot
        (
            [FromRoute] Guid slotId
        )
        {
            var responseDto = await _slotService.GetSlot(User, slotId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }


        [HttpPost]
        [Authorize(Roles = StaticUserRoles.Doctor)]
        public async Task<ActionResult<ResponseDTO>> CreateSlot
        (
            [FromBody] CreateSlotDTO createSlotDto
        )
        {
            var responseDto = await _slotService.CreateSlot(User, createSlotDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        [Authorize(Roles = StaticUserRoles.Doctor)]
        public async Task<ActionResult<ResponseDTO>> UpdateSlot
        (
            [FromBody] UpdateSlotDTO updateSlotDto
        )
        {
            var responseDto = await _slotService.UpdateSlot(User, updateSlotDto);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete]
        [Route("{slotId:guid}")]
        [Authorize(Roles = StaticUserRoles.Doctor)]
        public async Task<ActionResult<ResponseDTO>> DeleteSlot
        (
            [FromRoute] Guid slotId
        )
        {
            var responseDto = await _slotService.DeleteSlot(User, slotId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}