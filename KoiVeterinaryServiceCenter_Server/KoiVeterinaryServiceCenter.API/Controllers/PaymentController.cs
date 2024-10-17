using Microsoft.AspNetCore.Mvc;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Threading.Tasks;
using Net.payOS.Types;

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
        public async Task<IActionResult> CreatePaymentLink([FromBody] CreatePaymentRequest request)
        {
            try
            {
                var items = new List<ItemData>
                {
                     new ItemData("Iphone", 2, 28000000)
                };


                var paymentResult = await _paymentService.CreatePaymentLink(
                    orderCode: request.OrderCode,
                    amount: request.Amount,
                    description: request.Description,
                    cancelUrl: request.CancelUrl,
                    returnUrl: request.ReturnUrl,
                    items: items
                );

                return Ok(paymentResult);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class CreatePaymentRequest
    {
        public int OrderCode { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
