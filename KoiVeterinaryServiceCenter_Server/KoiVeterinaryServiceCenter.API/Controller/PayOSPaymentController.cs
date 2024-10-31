using Microsoft.AspNetCore.Mvc;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Threading.Tasks;
using Net.payOS.Types;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Payment;

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
                var responseDto = await _paymentService.CreatePayOSPaymentLink(User, createPaymentLinkDTO);

                return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost("create-payment-link-for-deposit-part-1")]
        public async Task<ActionResult<ResponseDTO>> CreatePayOSLinkForDepositPart1([FromBody] CreatePaymentLinkDTO createPaymentLinkDTO)
        {
            var responseDto = await _paymentService.CreatePayOSLinkForDepositPart1(User, createPaymentLinkDTO);

            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{paymentTransactionId:guid}/update-payment-status-for-appointment-deposit-part1")]
        public async Task<ActionResult<ResponseDTO>> UpdatePaymentStatusForAppointmentDepositPart1([FromRoute] Guid paymentTransactionId)
        {
            var responseDto = await _paymentService.UpdatePayOSPaymentStatusForDepositPart1(User, paymentTransactionId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost("create-payment-link-for-deposit-part-2")]
        public async Task<ActionResult<ResponseDTO>> CreatePayOSLinkForDepositPart2([FromBody] CreatePaymentLinkDTO createPaymentLinkDTO)
        {
            var responseDto = await _paymentService.CreatePayOSLinkForDepositPart2(User, createPaymentLinkDTO);

            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{paymentTransactionId:guid}/update-payment-status-for-appointment-deposit-part2")]
        public async Task<ActionResult<ResponseDTO>> UpdatePaymentStatusForAppointmentDepositPart2([FromRoute] Guid paymentTransactionId)
        {
            var responseDto = await _paymentService.UpdatePayOSPaymentStatusForDepositPart1(User, paymentTransactionId);
            return StatusCode(responseDto.StatusCode, responseDto);
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
