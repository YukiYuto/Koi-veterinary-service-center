using Microsoft.AspNetCore.Mvc;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Threading.Tasks;
using Net.payOS.Types;
using KoiVeterinaryServiceCenter.Model.DTO;
<<<<<<< HEAD:KoiVeterinaryServiceCenter_Server/KoiVeterinaryServiceCenter.API/Controllers/PayOSPaymentController.cs
=======
using KoiVeterinaryServiceCenter.Models.DTO;
>>>>>>> 61f9fcc6684779a3c22f7c4b2265172ff3dabef5:KoiVeterinaryServiceCenter_Server/KoiVeterinaryServiceCenter.API/Controller/PayOSPaymentController.cs

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOSPaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PayOSPaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-payment-link")]
        public async Task<ActionResult<ResponseDTO>> CreatePaymentLink([FromBody] CreatePaymentLinkDTO createPaymentLinkDTO)
        {
            try
            {
                var responseDto = await _paymentService.CreatePayOSPaymentLink(User, createPaymentLinkDTO);

                return StatusCode(responseDto.StatusCode, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{paymentTransactionId:guid}/update-payment-status")]
        public async Task<ActionResult<ResponseDTO>> UpdatePaymentStatus([FromRoute] Guid paymentTransactionId)
        {
            var responseDto = await _paymentService.UpdatePayOSPaymentStatus(User, paymentTransactionId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{paymentTrasactionId:guid}/cancel-payment-link")]
        public async Task<ActionResult<ResponseDTO>> CancelPaymentLink([FromRoute] Guid paymentTrasactionId, [FromBody] string? cancellationReason)
        {
            var responsDto = await _paymentService.CancelPayOSPaymentLink(User, paymentTrasactionId, cancellationReason);
            return StatusCode(responsDto.StatusCode, responsDto);
        }


    }
}
