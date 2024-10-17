using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.payOS.Types;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPaymentService
    {
        Task<CreatePaymentResult> CreatePaymentLink(int orderCode, int amount, string description, string cancelUrl, string returnUrl, List<ItemData> items);
    }
}
