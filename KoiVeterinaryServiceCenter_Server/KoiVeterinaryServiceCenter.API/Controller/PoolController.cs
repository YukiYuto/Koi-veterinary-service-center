using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAll
            (
                [FromQuery] string? filterOn,
                [FromQuery] string? filterQuery,
                [FromQuery] string? sortBy,
                [FromQuery] bool? isAscending,
                [FromQuery] int pageNumber,
                [FromQuery] int pageSize
            )
        {
            var responseDto = await _poolService.GetAll(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{poolId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetPoolById(Guid poolId)
        {
            var responseDto = await _poolService.GetPoolById(User, poolId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{customerId:guid}/customer")]
        public async Task<ActionResult<ResponseDTO>> GetPoolByCustomerId(string customerId)
        {
            var responseDto = await _poolService.GetPoolByCustomerId(User, customerId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreatePool(CreatePoolDTO createPoolDTO)
        {
            var responseDto = await _poolService.CreatePool(User, createPoolDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdatePool(UpdatePoolDTO updatePoolDTO)
        {
            var responseDto = await _poolService.UpdatePool(User, updatePoolDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
        [HttpPost("avatar")]
       
        public async Task<ActionResult<ResponseDTO>> UploadPoolAvatar(IFormFile file)
        {
            var responseDto = await _poolService.UploadPoolAvatar(file, User);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
