using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;
        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetRevenueList([FromQuery] int month, [FromQuery] int year)
        {
            var responseDto = await _dashBoardService.RevenuaOfMonthList(month, year);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
