using Microsoft.AspNetCore.Mvc;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Threading.Tasks;
using Net.payOS.Types;
using KoiVeterinaryServiceCenter.Model.DTO;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-payment-link")]
        public async Task<ActionResult<ResponseDTO>> CreatePaymentLink([FromBody] CreatePaymentLinkDTO createPaymentLinkDTO)
        {
            try
            {
                var responseDto = await _paymentService.CreatePaymentLink(User, createPaymentLinkDTO);

                return StatusCode(responseDto.StatusCode, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    //public class CreatePaymentRequest
    //{
    //    public int OrderCode { get; set; }
    //    public int Amount { get; set; }
    //    public string Description { get; set; }
    //    public string CancelUrl { get; set; }
    //    public string ReturnUrl { get; set; }
    //}
}
