using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Transaction;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var responseDto = await _transactionsService.GetAll(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{transactinId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetTransaction([FromRoute] Guid transactinId)
        {
            var responseDto = await _transactionsService.GetById(User, transactinId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateTransaction([FromBody] CreateTransactionDTO transactionDTO)
        {
            var responseDto = await _transactionsService.CreateTransaction(User, transactionDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
