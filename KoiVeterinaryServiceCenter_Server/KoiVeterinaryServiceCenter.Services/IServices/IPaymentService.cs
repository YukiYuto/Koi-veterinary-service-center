using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.DTO;
using Net.payOS.Types;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPaymentService
    {
        Task<ResponseDTO> CreatePayOSPaymentLink(ClaimsPrincipal User, CreatePaymentLinkDTO getPaymentLink);
        Task<ResponseDTO> UpdatePayOSPaymentStatus(ClaimsPrincipal User, Guid paymentTransactionId);
        Task<ResponseDTO> CancelPayOSPaymentLink(ClaimsPrincipal User, Guid paymentTransactionId, string cancellationReason);
    }
}
