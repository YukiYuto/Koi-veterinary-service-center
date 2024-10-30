using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolController : ControllerBase
    {
        private readonly IPoolService _poolService;
        public PoolController(IPoolService poolService)
        {
            _poolService = poolService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreatePool(CreatePoolDTO createPoolDTO)
        {
            var responseDto = await _poolService.CreatePool(User, createPoolDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
