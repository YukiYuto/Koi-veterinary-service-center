using Net.payOS;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.Extensions.Configuration;
using Net.payOS.Types;

public class PaymentService : IPaymentService
{
    private readonly PayOS _payOS;

    public PaymentService(IConfiguration configuration)
    {
        _payOS = new PayOS(
        configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find PAYOS_CLIENT_ID"),
        configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find PAYOS_API_KEY"),
        configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find PAYOS_CHECKSUM_KEY"),
        configuration["Environment:PAYOS_PARTNER_CODE"] ?? throw new Exception("Cannot find PAYOS_PARTNER_CODE")
    );
    }

    public async Task<CreatePaymentResult> CreatePaymentLink(int orderCode, int amount, string description, string cancelUrl, string returnUrl, List<ItemData> items)
    {
        var paymentData = new PaymentData(
            orderCode: orderCode,
            amount: amount,
            description: description,
            items: items,
            cancelUrl: cancelUrl,
            returnUrl: returnUrl
        );

        CreatePaymentResult result = await _payOS.createPaymentLink(paymentData);
        return result;
    }
}
